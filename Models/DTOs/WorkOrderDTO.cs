using System.ComponentModel.DataAnnotations;

namespace OrderManager.Models.DTOs
{
    public class WorkOrderDTO
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Required]
        public Client Client { get; set; }
    }
}
