using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Model
{
    [Table("Person")]
    public class Person
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("gender")]
        public string Gender { get; set; }
    }
}
