using WebApi.Client.Dto.OperationDto;
using WebApi.Client.Models;

namespace WebApi.Client.Services.IServices;

    public interface IReportService
    {
        Task<ReportDailyDto> GetDailyReportAsync(DateTime? date);
        Task<ReportPeriodDto> GetPeriodReportAsync(DateTime startDate, DateTime endDate);
    }
