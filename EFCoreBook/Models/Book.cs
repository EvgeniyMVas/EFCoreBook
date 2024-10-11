using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreBook.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string Author {  get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string Genre {  get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Цена книги обязательна для заполнения.")]
        [Range(0.01, 10000, ErrorMessage = "Цена должна быть положительной и не превышать 10000.")]
        public decimal Price {  get; set; }

        public ICollection<Comment> Comments { get; set; }= new List<Comment>();
    }
}
