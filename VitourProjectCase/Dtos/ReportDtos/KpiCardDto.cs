namespace VitourProjectCase.Dtos.ReportDtos
{
    public class KpiCardDto
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public string PreviousValue { get; set; }
        public double PercentageChange { get; set; }
        public bool IsIncrease { get; set; }
    }
}
