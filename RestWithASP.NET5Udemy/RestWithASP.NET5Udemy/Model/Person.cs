using RestWithASP.NET5Udemy.Model.Context.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Model
{
    [Table("Person")]
    public class Person : BaseEntity
    {

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("gender")]
        public string Gender { get; set; }
        [Column("enabled")]
        public bool Enabled { get; set; }
    }
}
