using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameInventoryManager.Models
{
    public class Games
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Game Name")]
        [MaxLength(50, ErrorMessage = "Game name too long")]
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "The game name can only contain letters, numbers and spaces.")]
        public string name { get; set; }

        [DisplayName("Price")]
        [Range(0, 1000, ErrorMessage = "The price must be between 0 and 1000 €.")]
        public double price { get; set; }

        [Required]
        [StringLength(15)]
        [DisplayName("Developper")]
        public string possessor { get; set; }

    }
}
