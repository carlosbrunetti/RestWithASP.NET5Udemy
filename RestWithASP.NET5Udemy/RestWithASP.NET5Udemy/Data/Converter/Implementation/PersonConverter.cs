using RestWithASP.NET5Udemy.Data.Converter.Contract;
using RestWithASP.NET5Udemy.Data.VO;
using RestWithASP.NET5Udemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASP.NET5Udemy.Data.Converter.Implementation
{
    public class PersonConverter : IParse<PersonVO, Person>, IParse<Person, PersonVO>
    {

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null)
                return null;

            return origin.Select(Item => Parse(Item)).ToList();

        }
        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null)
                return null;

            return origin.Select(Item => Parse(Item)).ToList();
        }
        public PersonVO Parse(Person origin)
        {
            if (origin == null)
                return null;

            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }
        public Person Parse(PersonVO origin)
        {
            if (origin == null)
                return null;

            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

    }
}
