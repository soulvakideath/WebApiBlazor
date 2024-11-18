namespace WebApi.Client.Dto.OperationDto;
    public class OperationDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public bool IsIncome { get; set; } 
    }
