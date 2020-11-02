using System.ComponentModel.DataAnnotations.Schema;


namespace RestWithASP.NET5Udemy.Model.Context.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
