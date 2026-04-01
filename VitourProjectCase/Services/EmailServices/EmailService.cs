using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using VitourProjectCase.Dtos.ReservationDtos;
using VitourProjectCase.Services.TourServices;

namespace VitourProjectCase.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly ITourService _tourService;

        public EmailService(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task SendConfirmReservationEmailAsync(CreateReservationDto createReservationDto)
        {
            var tour = await _tourService.GetTourByIdAsync(createReservationDto.TourId);
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Vitour", "email");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", createReservationDto.Email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = $"Rezervasyon Onayı - {createReservationDto.ReservationCode}";
            // HTML template
            string htmlBody = $@"
        <html>
        <head>
            <style>
                body {{ font-family: 'Segoe UI', sans-serif; background:#f4f4f4; }}
                .container {{ max-width:600px; margin:auto; background:white; padding:20px; border-radius:8px; }}
                .header {{ background:#16a34a; color:white; padding:10px 20px; border-radius:8px 8px 0 0; }}
                .header h1 {{ margin:0; font-size:24px; }}
                .content {{ padding:20px; }}
                .content h2 {{ color:#16a34a; }}
                .content p {{ line-height:1.5; }}
                .btn {{ display:inline-block; background:#16a34a; color:white; padding:10px 20px; border-radius:5px; text-decoration:none; }}
                .details {{ background:#f0fdf4; padding:10px; border-radius:5px; margin-top:10px; }}
                .details span {{ font-weight:600; }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'><h1>Rezervasyon Onayınız</h1></div>
                <div class='content'>
                    <img src='{tour.CoverImageUrl}' style='width:100%; border-radius:8px; margin-bottom:15px;' />
                    <h2>Merhaba {createReservationDto.NameSurname},</h2>
                    <p>Rezervasyonunuz başarıyla tamamlandı. Detaylar aşağıdadır:</p>
                    <div class='details'>
                        <p><span>Tur:</span> {tour.Title}</p>
                        <p><span>Kişi Sayısı:</span> {createReservationDto.PersonCount}</p>
                        <p><span>Toplam Fiyat:</span> {createReservationDto.TotalPrice:N2} ₺</p>
                        <p><span>Tarih:</span> {createReservationDto.ReservationDate:dd.MM.yyyy}</p>                        
                        <p><span>Rezervasyon Kodu:</span> {createReservationDto.ReservationCode}</p>
                    </div>
                    <p>Rezervasyonu görüntülemek için:</p>
                    <a class='btn' href='https://yoursite.com/reservation/{createReservationDto.ReservationCode}'>Rezervasyonu Görüntüle</a>
                </div>
            </div>
        </body>
        </html>";
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlBody };

            SmtpClient smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtpClient.AuthenticateAsync("email", "apikey");
            await smtpClient.SendAsync(mimeMessage);
            await smtpClient.DisconnectAsync(true);
        }
    }
}
