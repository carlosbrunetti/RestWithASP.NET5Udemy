using RestWithASP.NET5Udemy.Hypermedia;
using RestWithASP.NET5Udemy.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
    {
        //[JsonPropertyName]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
