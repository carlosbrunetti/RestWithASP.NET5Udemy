using RestWithASP.NET5Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Business
{
    public interface IBooksBusiness
    {
        Book Create(Book books);
        Book FindByID(long id);
        void Delete(long id);
        Book Update(Book books);
        List<Book> FindAll();
    }
}
