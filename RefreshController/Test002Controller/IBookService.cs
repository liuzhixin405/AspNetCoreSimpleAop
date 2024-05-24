using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test002Controller
{
    public interface IBookService
    {
        string GetBookName();
    }

    public class BookService : IBookService
    {
        public string GetBookName()
        {
            return "枫林玉露";
        }
    }
}
