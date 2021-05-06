using BookStoreMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.Controllers.api
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Json(new { data = await _context.Books.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookInDb = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (bookInDb == null)
                return Json(new{status=false,msg="Data not found." });
             _context.Remove(bookInDb);
            await _context.SaveChangesAsync();
            return Json(new { status = true, msg = "Book is deleted successfully." });

        }
    }
}
