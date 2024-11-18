

using WebApi.Dto.OperationDto;

public class ReportPeriodDto
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public IEnumerable<OperationDto> Operations { get; set; }
}