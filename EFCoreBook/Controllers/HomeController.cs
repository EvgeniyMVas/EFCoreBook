using EFCoreBook.Data;
using EFCoreBook.Models;
using EFCoreBook.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCoreBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;
        public HomeController(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {
               return View(book);
            }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var currentBook = await _context.Books
                    .Include(b => b.Comments)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (currentBook != null)
                {
                    var viewModel = new BookDetailsViewModel
                    {
                        Book = currentBook,
                        Comment = new Comment { BookId = currentBook.Id }
                    };

                    return View(viewModel);
                }
            }

            return NotFound();
        }

        public IActionResult WriteAComment(int bookId)
        {
            var comment = new Comment { BookId = bookId };  
            return PartialView(comment);  
        }
        [HttpPost]
        
        public async Task<IActionResult> WriteAComment(Comment comment)
        {


            //if (!ModelState.IsValid)
            //{
            //    // Логируем ошибки валидации
            //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //    {
            //        Console.WriteLine($"Validation error: {error.ErrorMessage}");
            //    }
            //    var currentBook = await _context.Books
            //        .Include(b => b.Comments)
            //        .FirstOrDefaultAsync(b => b.Id == comment.BookId);

            //    if (currentBook == null)
            //    {
            //        return NotFound();
            //    }
            //    var viewModel = new BookDetailsViewModel
            //    {
            //        Book = currentBook,
            //        Comment = comment
            //    };

            //    return View("Details", viewModel);
            //}


            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = comment.BookId });
        }
        //Придумать дефолтные значения чтобы убрать валидацию!!! Не отправляет форму так как при попытке
        //проверки if (!ModelState.IsValid) постоянно возникает ошибка:"Validation error: The Book field is required.",
        //Ошибка происходит, потому что валидация пытается проверить поле Book, которое является навигационным
        //свойством. Не удалось решить с помощью ModelState.Remove("Book"); и [NotMapped]. Попытки исключить поле из
        //валидации к решению проблемы не привели. 
        //Леонид Валерьевич если не трудно прокомментируйте этот момент при разборе ДЗ на паре, думаю всем пригодится.
    }
}
