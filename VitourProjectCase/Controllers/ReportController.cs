using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using VitourProjectCase.Services.CategoryServices;
using VitourProjectCase.Services.ReservationServices;
using VitourProjectCase.Services.ReviewServices;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITourService _tourService;
        private readonly IReviewService _reviewService;
        private readonly ICategoryService _categoryService;

        public ReportController(IReservationService reservationService, ITourService tourService, IReviewService reviewService, ICategoryService categoryService)
        {
            _reservationService = reservationService;
            _tourService = tourService;
            _reviewService = reviewService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> DownloadReservationsPdf(string tourId)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var tour = await _tourService.GetTourByIdAsync(tourId);
            var reservations = await _reservationService.GetReservationsByTourIdAsync(tourId);

            using var stream = new MemoryStream();
            var doc = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter.GetInstance(doc, stream);
            doc.Open();

            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            Font titleFont = new Font(baseFont, 16, Font.BOLD);
            Font headerFont = new Font(baseFont, 12, Font.BOLD);
            Font rowFont = new Font(baseFont, 11, Font.NORMAL);

            doc.Add(new Paragraph($"Tur Rezervasyon Listesi - {tour.Title} (#{tourId})", titleFont));
            doc.Add(new Paragraph("\n"));

            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 3f, 4f, 2f, 3f });

            table.AddCell(new PdfPCell(new Phrase("İsim", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Email", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Kişi Sayısı", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Rezervasyon Kodu", headerFont)));

            foreach (var r in reservations)
            {
                table.AddCell(new PdfPCell(new Phrase(r.NameSurname, rowFont)));
                table.AddCell(new PdfPCell(new Phrase(r.Email, rowFont)));
                table.AddCell(new PdfPCell(new Phrase(r.PersonCount.ToString(), rowFont)));
                table.AddCell(new PdfPCell(new Phrase(r.ReservationCode, rowFont)));
            }
            doc.Add(table);
            doc.Close();

            byte[] bytes = stream.ToArray();
            return File(bytes, "application/pdf", $"Tur_{tourId}_Reservations.pdf");
        }
       
        public async Task<IActionResult> DownloadReviewsExcel()
        {
            var reviews = await _reviewService.GetAllReviewAsync();
            var tours = await _tourService.GetAllTourAsync();

            var tourDict = tours.ToDictionary(x => x.TourId, x => x.Title);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Yorumlar");

                worksheet.Cell(1, 1).Value = "İsim";
                worksheet.Cell(1, 2).Value = "Yorum";
                worksheet.Cell(1, 3).Value = "Puan";
                worksheet.Cell(1, 4).Value = "Tarih";
                worksheet.Cell(1, 5).Value = "Durum";
                worksheet.Cell(1, 6).Value = "Tur Adı";

                var headerRange = worksheet.Range(1, 1, 1, 6);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int row = 2;

                foreach (var r in reviews)
                {
                    var tourTitle = tourDict.ContainsKey(r.TourId) ? tourDict[r.TourId] : "Bilinmiyor";

                    worksheet.Cell(row, 1).Value = r.NameSurname;
                    worksheet.Cell(row, 2).Value = r.Comment;
                    worksheet.Cell(row, 3).Value = r.Score;
                    worksheet.Cell(row, 4).Value = r.ReviewDate.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 5).Value = r.Status ? "Aktif" : "Pasif";
                    worksheet.Cell(row, 6).Value = tourTitle;

                    row++;
                }
                worksheet.Columns().AdjustToContents();

                var table = worksheet.Range(1, 1, row - 1, 6).CreateTable();
                table.Theme = XLTableTheme.TableStyleMedium9;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Yorumlar.xlsx");
                }
            }
        }

        public async Task<IActionResult> DownloadToursExcel()
        {
            var tours = await _tourService.GetAllTourAsync();
            var categories = await _categoryService.GetAllCategoryAsync();
            var categoryDict = categories.ToDictionary(x => x.CategoryId, x => x.CategoryName);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Turlar");

                worksheet.Cell(1, 1).Value = "#ID";
                worksheet.Cell(1, 2).Value = "Tur Adı";
                worksheet.Cell(1, 3).Value = "Kategori";
                worksheet.Cell(1, 4).Value = "Süre";
                worksheet.Cell(1, 5).Value = "Kapasite";
                worksheet.Cell(1, 6).Value = "Fiyat";
                worksheet.Cell(1, 7).Value = "Durum";

                var headerRange = worksheet.Range(1, 1, 1, 7);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int row = 2;

                foreach (var t in tours)
                {
                    var categoryName = categoryDict.ContainsKey(t.CategoryId) ? categoryDict[t.CategoryId] : "Bilinmiyor";

                    worksheet.Cell(row, 1).Value = t.TourId;
                    worksheet.Cell(row, 2).Value = t.Title;
                    worksheet.Cell(row, 3).Value = categoryName;
                    worksheet.Cell(row, 4).Value = t.DayCount;
                    worksheet.Cell(row, 5).Value = t.Capacity;
                    worksheet.Cell(row, 6).Value = t.Price;
                    worksheet.Cell(row, 7).Value = t.Status ?"Aktif":"Pasif";

                    row++;
                }

                worksheet.Columns().AdjustToContents();

                var table = worksheet.Range(1, 1, row - 1, 7).CreateTable();
                table.Theme = XLTableTheme.TableStyleMedium9;

                using(var stream=new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content,
                                   "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                   "Turlar.xlsx");
                }
            }
        }

        public async Task<IActionResult> DownloadCategoriesPdf()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var categories = await _categoryService.GetAllCategoryAsync();

            using var stream = new MemoryStream();
            var doc = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter.GetInstance(doc, stream);
            doc.Open();

            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            Font titleFont = new Font(baseFont, 16, Font.BOLD);
            Font headerFont = new Font(baseFont, 12, Font.BOLD);
            Font rowFont = new Font(baseFont, 11, Font.NORMAL);

            doc.Add(new Paragraph("Kategori Listesi", titleFont));
            doc.Add(new Paragraph("\n"));

            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 3F,3f, 2f });

            table.AddCell(new PdfPCell(new Phrase("Kategori ID", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Kategori Adı", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Durum", headerFont)));

            foreach(var c in categories)
            {
                table.AddCell(new PdfPCell(new Phrase(c.CategoryId, headerFont)));
                table.AddCell(new PdfPCell(new Phrase(c.CategoryName, rowFont)));
                table.AddCell(new PdfPCell(new Phrase(c.CategoryStatus?"Aktif":"Pasif", rowFont)));
            }
            doc.Add(table);
            doc.Close();

            byte[] bytes = stream.ToArray();
            return File(bytes, "application/pdf", "Kategoriler.pdf");
        }
        public IActionResult ReportList()
        {
            return View();
        }
    }
}
