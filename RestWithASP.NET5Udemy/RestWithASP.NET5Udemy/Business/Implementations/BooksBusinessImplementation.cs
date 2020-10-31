using RestWithASP.NET5Udemy.Model;
using RestWithASP.NET5Udemy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Business.Implementations
{
    public class BooksBusinessImplementation : IBooksBusiness
    {
        private readonly IBooksRepository _repository;

        public BooksBusinessImplementation(IBooksRepository repository)
        {
            _repository = repository;
        }
        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public Book Create(Book books)
        {
            return _repository.Create(books);
        }

        public Book Update(Book books)
        {
            return _repository.Update(books);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }



    }
}
