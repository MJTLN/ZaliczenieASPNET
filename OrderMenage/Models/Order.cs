using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderMenage.Models
{
	public class Order
	{
		[Key]
        [Required]
        public int Id { get; set; }

		[DisplayName("ID produktu")]
        [Required(ErrorMessage = "Pole wymagane")]
		public int ProductId { get; set; }

        [DisplayName("Ilość")]
        [Required(ErrorMessage = "Pole wymagane")]
        public int Size { get; set; }


        [DisplayName("Należność")]
        [Required(ErrorMessage = "Pole wymagane")]
        public double Amount { get; set; }

        [DisplayName("Imię i Nazwisko odbiorcy")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string CustomerName { get; set; }

        [DisplayName("Numer telefonu odbiorcy")]
        [Required(ErrorMessage = "Pole wymagane")]
        [RegularExpression("(\\+\\d{2})?\\s?(\\d{3})\\s?-?(\\d{3})\\s?-?(\\d{3})",
            ErrorMessage = "Podano nieprawidłowy numer telefonu")]
        public string CustomerPhone { get; set; }

        [DisplayName("Email odbiorcy")]
        [Required(ErrorMessage = "Pole wymagane")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres Email")]
        public string CustomerMail { get; set; }

        [DisplayName("Adres dostawy")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Address { get; set; }

        [DisplayName("Data Zamówienia")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime OrderTime { get; set; }
	}
}

