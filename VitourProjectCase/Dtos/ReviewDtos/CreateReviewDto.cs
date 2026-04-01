namespace VitourProjectCase.Dtos.ReviewDtos
{
    public class CreateReviewDto
    {
        public string NameSurname { get; set; }
        public string Comment { get; set; }
        public double Score { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool Status { get; set; }
        public string TourId { get; set; }

        public int GuideScore { get; set; }
        public int TransportationScore { get; set; }
        public int AccommodationScore { get; set; }
        public int ValueForMoneyScore { get; set; }

    }
}
