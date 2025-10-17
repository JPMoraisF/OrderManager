using System.ComponentModel.DataAnnotations;

namespace OrderManager.Models.DTOs
{
    public class ClientDTO
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

    }
}
