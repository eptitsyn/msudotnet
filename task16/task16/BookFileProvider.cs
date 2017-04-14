using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace task16
{
    public class BookFileProvider : IBookProvider
    {
        private const string fileName = "books.xml";
        private readonly string fullFileName = Path.Combine(Environment.CurrentDirectory, fileName);

        public Book[] GetBooks()
        {
            Book[] resultBooks;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Book[]));
            using (StreamReader streamReader = new StreamReader(fullFileName))
            {
                resultBooks = (Book[]) (xmlSerializer.Deserialize(streamReader));
            }
            return resultBooks;
        }

        public void SaveBooks(Book[] books)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Book[]));
            using (StreamWriter streamWriter = new StreamWriter(fullFileName))
            {
                xmlSerializer.Serialize(streamWriter, books);
            }
        }
    }
}
