using RestWithASP.NET5Udemy.Model.Context.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindByID(long id);
        void Delete(long id);
        T Update(T item);
        List<T> FindAll();
        bool Exists(long id);
        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
