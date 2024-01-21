using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderMenage.Models
{
	public class Product
	{
        [DisplayName("Nazwa produktu")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Name { get; set; }
        [Key]
        [Required]
        public int Id { get; set; }
        [DisplayName("Typ Produktu")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Type { get; set; }
        [DisplayName("Cena za sztukę")]
        [Required(ErrorMessage = "Pole wymagane")]
        public int Price { get; set; }
	}
}

