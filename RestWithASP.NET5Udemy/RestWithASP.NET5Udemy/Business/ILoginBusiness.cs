using RestWithASP.NET5Udemy.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredencials(UserVO user);
        TokenVO ValidateCredencials(TokenVO token);
        bool RevokeToken(string username);
    }
}
