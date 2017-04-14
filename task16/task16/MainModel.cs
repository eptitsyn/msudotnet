using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task16
{
    class MainModel
    {
        public Book[] Books { get; set; }

        public void New()
        {
            Books = new Book[0];
        }

        public void Load()
        {
            IBookProvider provider = new BookFileProvider();
            Books = provider.GetBooks();
        }

        public void Save()
        {
            IBookProvider provider = new BookFileProvider();
            provider.SaveBooks(Books);
        }
    }
}
