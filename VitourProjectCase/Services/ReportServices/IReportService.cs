using VitourProjectCase.Dtos.ReportDtos;

namespace VitourProjectCase.Services.ReportServices
{
    public interface IReportService
    {
        Task<List<KpiCardDto>> GetKpiCardsAsync();
        Task<List<MonthlyRevenueDto>> GetMonthlyRevenueChartAsync();
        Task<List<ResultTopTourDto>> GetTopTourListAsync();
        Task<List<MonthlyGoalDto>> GetMonthlyGoalsAsync();
    }
}
