using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Hypermedia.Filters
{
    public class HyperMediaFilter : ResultFilterAttribute
    {
        private readonly HyperMediaFIlterOptions _hyperMediaFIlterOptions;

        public HyperMediaFilter(HyperMediaFIlterOptions hyperMediaFIlterOptions)
        {
            _hyperMediaFIlterOptions = hyperMediaFIlterOptions;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            tryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void tryEnrichResult(ResultExecutingContext context)
        {
            if(context.Result is OkObjectResult objectResult)
            {
                var enricher = _hyperMediaFIlterOptions
                    .ContenResponseEnricherList.FirstOrDefault(x => x.CanEnrich(context));

                if (enricher != null) Task.FromResult(enricher.Enrich(context));

            };
        }
    }
}
