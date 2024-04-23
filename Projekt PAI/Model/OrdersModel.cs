using System.ComponentModel.DataAnnotations;

namespace Projekt_PAI.Model
{
    public class OrdersModel
    {
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        
        public string UserId { get; set; }
    }
}
