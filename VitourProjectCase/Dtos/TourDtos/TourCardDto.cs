namespace VitourProjectCase.Dtos.TourDtos
{
    public class TourCardDto
    {
        public string TourId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public string Badge { get; set; }
        public int DayCount { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ReviewCount { get; set; }
        public double ReviewScore { get; set; }
        public int GalleryCount { get; set; }

    }
}
