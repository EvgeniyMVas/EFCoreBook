using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreBook.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string Review { get; set; } = "Пойдет";
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string Name { get; set; } = "Аноним";
        [ForeignKey("Book")]
        public int BookId { get; set; }
        //[NotMapped]
        public Book Book { get; set; }
        //Придумать дефолтные значения чтобы убрать валидацию!!! Не отправляет форму так как при попытке
        //проверки if (!ModelState.IsValid) постоянно возникает ошибка:"Validation error: The Book field is required.",
        //Ошибка происходит, потому что валидация пытается проверить поле Book, которое является навигационным
        //свойством. Не удалось решить с помощью ModelState.Remove("Book"); и [NotMapped]. Попытки исключить поле из
        //валидации к решению проблемы не привели. 
        //Леонид Валерьевич если не трудно прокомментируйте этот момент при разборе ДЗ на паре, думаю всем пригодится.
    }
}
