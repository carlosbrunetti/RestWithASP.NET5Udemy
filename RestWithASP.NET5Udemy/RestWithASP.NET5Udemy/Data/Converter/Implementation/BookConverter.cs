using RestWithASP.NET5Udemy.Data.Converter.Contract;
using RestWithASP.NET5Udemy.Data.VO;
using RestWithASP.NET5Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Data.Converter.Implementation
{
    public class BookConverter : IParse<BookVO, Book>, IParse<Book, BookVO>
    {

        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null)
                return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null)
                return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        public BookVO Parse(Book origin)
        {
            if (origin == null)
                return null;
            
            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public Book Parse(BookVO origin)
        {
            if (origin == null)
                return null;

            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

    }
}
