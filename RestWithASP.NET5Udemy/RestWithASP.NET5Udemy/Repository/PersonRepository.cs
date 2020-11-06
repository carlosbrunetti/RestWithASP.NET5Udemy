using RestWithASP.NET5Udemy.Model;
using RestWithASP.NET5Udemy.Model.Context;
using RestWithASP.NET5Udemy.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context) { }

        public Person Disabled(long id)
        {
            if (!_context.People.Any(p => p.Id.Equals(id)))
                return null;

            var user = _context.People.SingleOrDefault(p => p.Id.Equals(id));

            if(user != null)
            {
                user.Enabled = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return user;
        }
    }
}
