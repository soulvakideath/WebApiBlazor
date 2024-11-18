using WebApi.Dto.OperationDto;

namespace WebApi.Services.IServices;

public interface IReportService
{
    Task<ReportPeriodDto> GetPeriodReportAsync(DateTime start, DateTime end);
    Task<ReportDailyDto> GetDailyReportAsync(DateTime date);
}