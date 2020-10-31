using RestWithASP.NET5Udemy.Model;
using System.Collections.Generic;

namespace RestWithASP.NET5Udemy.Business
{
    public interface IPersonBusiness
    {
        Person Create(Person person);
        Person FindByID(long id);
        void Delete(long id);
        Person Update(Person person);
        List<Person> FindAll();
             
    }
}
