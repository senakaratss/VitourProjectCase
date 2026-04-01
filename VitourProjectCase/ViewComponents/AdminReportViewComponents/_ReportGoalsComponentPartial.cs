using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VitourProjectCase.Services.ReportServices;

namespace VitourProjectCase.ViewComponents.AdminReportViewComponents
{
    public class _ReportGoalsComponentPartial:ViewComponent
    {
        private readonly IReportService _reportService;

        public _ReportGoalsComponentPartial(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _reportService.GetMonthlyGoalsAsync();
            return View(values);
        }
    }
}
