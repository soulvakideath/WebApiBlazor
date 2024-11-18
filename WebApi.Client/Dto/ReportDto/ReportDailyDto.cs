using WebApi.Client.Dto.OperationDto;

public class ReportDailyDto
{
    public DateTime? Date { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public IEnumerable<OperationDto> Operations { get; set; }
}