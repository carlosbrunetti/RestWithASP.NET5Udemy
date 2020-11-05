using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Data.VO
{
    public class UserVO
    {
        public string Password { get; internal set; }
        public string UserName { get; internal set; }
    }
}
