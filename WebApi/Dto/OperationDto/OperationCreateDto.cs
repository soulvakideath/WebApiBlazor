namespace WebApi.Dto.OperationDto;

public class OperationCreateDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int TypeId { get; set; } 
}