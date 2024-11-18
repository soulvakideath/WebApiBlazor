namespace WebApi.Dto.TypeDtos;

public class TypeUpdateDto
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsIncome { get; set; }
}