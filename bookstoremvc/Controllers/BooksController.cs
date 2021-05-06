using BookStoreMVC.Models;
using BookStoreMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookStoreMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var bookViewModel = new BookViewModel { Books = _context.Books };
            return View(bookViewModel);
        }

     
        public IActionResult Upsert()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Book model)
        {
            if(ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public  IActionResult Update(int id)
        {
            var bookInDb = _context.Books.FirstOrDefault(b => b.Id == id);
            if (bookInDb == null)
                return NotFound();
            return View(bookInDb);
        }

        [HttpPost]
        public IActionResult Update(Book model)
        {
            var bookInDb = _context.Books.FirstOrDefault(b => b.Id == model.Id);
            if (bookInDb == null)
                return NotFound();
            bookInDb.ISBN = model.ISBN;
            bookInDb.Author = model.Author;
            bookInDb.Name = model.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

       
        public IActionResult Delete(int id)
        {
            var bookInDb = _context.Books.FirstOrDefault(b => b.Id == id);
            if (bookInDb == null)
                return NotFound();

            _context.Remove(bookInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
