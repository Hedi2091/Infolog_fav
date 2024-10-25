using System.ComponentModel.DataAnnotations;

namespace infolog_asp_fab.Models
{
    public class Client
    {
        [Key] // Ajoutez cette ligne pour définir IdClients comme clé primaire
        public int IdClients { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
