namespace WebApi.Client.Dto.OperationDto;

public class OperationCreateDto
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime? Date { get; set; }
    public int TypeId { get; set; } 
}