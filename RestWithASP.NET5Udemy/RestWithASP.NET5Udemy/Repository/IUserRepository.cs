using RestWithASP.NET5Udemy.Data.VO;
using RestWithASP.NET5Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Repository
{
    public interface IUserRepository
    {
        User ValidateCredencials(UserVO user);
        User ResfreshUserInfo(User user);
    }
}
