using RestWithASP.NET5Udemy.Model;
using RestWithASP.NET5Udemy.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disabled(long id);

        List<Person> FindByName(string firstName, string secondName);
    }
}
