using RestWithASP.NET5Udemy.Data.VO;
using RestWithASP.NET5Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Business
{
    public interface IBooksBusiness
    {
        BookVO Create(BookVO books);
        BookVO FindByID(long id);
        void Delete(long id);
        BookVO Update(BookVO books);
        List<BookVO> FindAll();
    }
}
