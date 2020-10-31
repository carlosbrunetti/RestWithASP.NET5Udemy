using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Model
{
    [Table("Books")]
    public class Book
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("Author")]
        public string Author { get; set; }

        [Column("LaunchDate")]
        public DateTime LaunchDate { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("Title")]
        public string Title { get; set; }
    }
}
