using WebApi.Client.Dto.OperationDto;
using WebApi.Client.Models;
using WebApi.Client.Services.IServices;

namespace WebApi.Client.Services;

    public class ReportService : IReportService
    {
        private readonly HttpService _httpService;
        private readonly string _baseUrl = "api/report";

        public ReportService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ReportDailyDto> GetDailyReportAsync(DateTime? date)
        {
            var result = await _httpService.GetAsync<ReportDailyDto>($"{_baseUrl}/Daily?date={date:yyyy-MM-dd}");
            if (result.IsSuccess)
            {
                return result.Value;
            }
            else
            {
                throw new Exception(result.ErrorMessage);
            }
        }

        public async Task<ReportPeriodDto> GetPeriodReportAsync(DateTime startDate, DateTime endDate)
        {
            var result = await _httpService.GetAsync<ReportPeriodDto>($"{_baseUrl}/Period?start={startDate:yyyy-MM-dd}&end={endDate:yyyy-MM-dd}");
            if (result.IsSuccess)
            {
                return result.Value;
            }
            else
            {
                throw new Exception(result.ErrorMessage);
            }
        }
    }
