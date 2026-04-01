namespace VitourProjectCase.Dtos.ReportDtos
{
    public class MonthlyGoalDto
    {
        public string Name { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal TargetValue { get; set; }
        public string Unit { get; set; } // TL, rezervasyon, müşteri vb.
    }
}
