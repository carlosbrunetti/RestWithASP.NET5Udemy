using RestWithASP.NET5Udemy.Data.Converter.Implementation;
using RestWithASP.NET5Udemy.Data.VO;
using RestWithASP.NET5Udemy.Hypermedia.Utils;
using RestWithASP.NET5Udemy.Model;
using RestWithASP.NET5Udemy.Repository;
using RestWithASP.NET5Udemy.Repository.Generic;
using System.Collections.Generic;

namespace RestWithASP.NET5Udemy.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;
        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity =  _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PersonVO Disabled(long id)
        {
            var personEntity = _repository.Disabled(id);
            return _converter.Parse(personEntity);
        }

        public List<PersonVO> FindByName(string firstName, string secondeName)
        {
            return _converter.Parse(_repository.FindByName(firstName, secondeName));
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(
            string name, string sortDirection, int size, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) &&
                !sortDirection.Equals("desc")) ? "asc" : "desc";
            var pageSize = (size < 1) ? 10 : size;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1 = 1 ";
            string countQuery = @"select count(*) from person p where 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(name))
            {
                query += $" and first_name like '%{name}%' ";
                countQuery += $" and first_name like '%{name}%' ";
            }
            query += $" order by p.first_Name {sort} limit {size} offset {offset}";

            var people = _repository.FindWithPagedSearch(query);
            int totalResult = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Parse(people),
                SortDirections = sort,
                TotalResults = totalResult
            };
        }
    }
}
