using RestWithASP.NET5Udemy.Model;
using RestWithASP.NET5Udemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Repository.Implementations
{
    public class BooksRepositoryImplementation : IBooksRepository
    {
        private MySQLContext _context;
        public BooksRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public Book FindByID(long id)
        {
            return _context.Books.FirstOrDefault(b => b.Id.Equals(id));
        }

        public Book Create(Book books)
        {
            try
            {
                _context.Add(books);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return books;
        }
        public Book Update(Book books)
        {
            try
            {
                if (!Exists(books.Id))
                    return null;

                var result = _context.Books.SingleOrDefault(b => b.Id.Equals(books.Id));
                
                if (result != null)
                {
                    _context.Entry(result).CurrentValues.SetValues(books);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return books;
        }


        public void Delete(long id)
        {
            try
            {
                var result = _context.Books.SingleOrDefault(b => b.Id.Equals(id));
                if (result != null)
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool Exists(long id)
        {
            return _context.Books.Any(p => p.Id.Equals(id));
        }
    }
}
