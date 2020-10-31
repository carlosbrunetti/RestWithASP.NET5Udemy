using RestWithASP.NET5Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Repository
{
    public interface IBooksRepository
    {
        Book Create(Book book);
        Book FindByID(long id);
        void Delete(long id);
        Book Update(Book book);
        List<Book> FindAll();
        bool Exists(long id);
    }
}
