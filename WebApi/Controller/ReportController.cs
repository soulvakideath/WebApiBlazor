using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.OperationDto;
using WebApi.Services.IServices;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
         _reportService = reportService;
    }

    [HttpGet("Daily")]
    public async Task<ActionResult<ReportDailyDto>> GetDailyReport(DateTime date)
    {
        if (date == default)
        {
            return BadRequest("Invalid date");
        }

        var report = await _reportService.GetDailyReportAsync(date);

        if (!report.Operations.Any())
        {
            return NotFound("No operations found for the given date");
        }

        return Ok(report);
    }

    [HttpGet("Period")]
    public async Task<ActionResult<ReportPeriodDto>> GetPeriodReport(DateTime start, DateTime end)
    {
        if (start == default || end == default)
        {
            return BadRequest("Invalid start or end date");
        }

        if (start > end)
        {
            return BadRequest("Start date must be earlier than end date");
        }

        var report = await _reportService.GetPeriodReportAsync(start, end);

        if (!report.Operations.Any())
        {
            return NotFound("No operations found for the given period");
        }

        return Ok(report);
    }
}
