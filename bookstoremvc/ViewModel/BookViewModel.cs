using BookStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.ViewModel
{
    public class BookViewModel
    {
        public IEnumerable<Book> Books { get; set; }

    }
}
