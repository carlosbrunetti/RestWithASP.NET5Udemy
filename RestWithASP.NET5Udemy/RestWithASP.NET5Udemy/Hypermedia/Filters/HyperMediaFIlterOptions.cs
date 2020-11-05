using RestWithASP.NET5Udemy.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Hypermedia.Filters
{
    public class HyperMediaFIlterOptions
    {
        public List<IResponseEnricher> ContenResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
