using RestWithASP.NET5Udemy.Data.VO;
using RestWithASP.NET5Udemy.Model;
using System.Collections.Generic;

namespace RestWithASP.NET5Udemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);
        void Delete(long id);
        PersonVO Update(PersonVO person);
        PersonVO Disabled(long id);
        List<PersonVO> FindAll();
             
    }
}
