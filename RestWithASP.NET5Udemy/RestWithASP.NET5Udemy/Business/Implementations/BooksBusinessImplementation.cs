using RestWithASP.NET5Udemy.Data.Converter.Contract;
using RestWithASP.NET5Udemy.Data.Converter.Implementation;
using RestWithASP.NET5Udemy.Data.VO;
using RestWithASP.NET5Udemy.Model;
using RestWithASP.NET5Udemy.Repository;
using RestWithASP.NET5Udemy.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Business.Implementations
{
    public class BooksBusinessImplementation : IBooksBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;
        public BooksBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }
        public List<BookVO> FindAll()
        {

            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindByID(long id)
        {
            return _converter.Parse( _repository.FindByID(id));
        }

        public BookVO Create(BookVO books)
        {
            var bookEntity = _converter.Parse(books);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO books)
        {
            var bookEntity = _converter.Parse(books);
            bookEntity =  _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }



    }
}
