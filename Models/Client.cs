using System.ComponentModel.DataAnnotations;

namespace OrderManager.Models
{
    // TODO: Refactor this to be more DDD like with methods to manipulate the Client entity itself
    public class Client
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

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

        public DateTime CreatedAt { get; set; }

    }
}
