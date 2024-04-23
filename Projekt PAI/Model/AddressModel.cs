using System.ComponentModel.DataAnnotations;

namespace Projekt_PAI.Model
{
    public class AddressModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [RegularExpression(@"^[A-ZĄĆĘŁŃÓŚŹŻa-ząćęłńóśźż]+\s\d+$", ErrorMessage = "Błędny adres")]
        public string StreetAndNr { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Błędny kod pocztowy")]
        public string PostalCode { get; set; }

        public string UserId { get; set; }
    }
}
