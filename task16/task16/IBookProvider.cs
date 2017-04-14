using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task16
{
    public interface IBookProvider
    {
        Book[] GetBooks();
        void SaveBooks(Book[] books);
    }
}
