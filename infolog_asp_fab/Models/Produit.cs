using System.ComponentModel.DataAnnotations;

namespace infolog_asp_fab.Models
{
    public class Produit
    {
        [Key] // Ajoutez cette ligne pour définir IdProduits comme clé primaire
        public int IdProduits { get; set; } // Correspond à la colonne "IdProduits"

        [Required]
        public string ProductName { get; set; } // Correspond à la colonne "ProductName"

        [Required]
        public string Description { get; set; } // Correspond à la colonne "Description"

        [Required]
        public string Price { get; set; } // Correspond à la colonne "Price"

        [Required]
        public string Quantity { get; set; } // Correspond à la colonne "Quantity"

        [Required]
        public string Category { get; set; } // Correspond à la colonne "Category"
    }
}
