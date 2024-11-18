using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Operation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public TransactionType Type { get; set; }
    }
}
