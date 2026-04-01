namespace VitourProjectCase.Dtos.TourPlanDtos
{
    public class UpdateTourPlanDto
    {
        public string TourPlanId { get; set; }
        public int DayNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TourId { get; set; }
    }
}
