using Sitecore.Data;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Safe.Common.Helper;
using Sitecore.Safe.Models;
using Sitecore.Safe.Security.QueryString;
using Sitecore.Safe.Settings;
using System;
using System.Linq;
using System.Web;

namespace Sitecore.Safe.Processors
{
    public class QueryString : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {

            if (!SitecoreHelper.IsValidSiteForPipeline())
            {
                return;
            }

            var queryStringThreat = ProcessUrl.GetQueryStringThreat();

            // Write response
            if (queryStringThreat!=null && queryStringThreat.IsThreat)
            {
                var notFoundPage = Context.Database.GetItem(new ID(queryStringThreat.ThreatPageId));

                if (notFoundPage == null)
                    return;

                args.ProcessorItem = notFoundPage;
                args.HttpContext.Response.StatusCode = 400;
                Context.Item = notFoundPage;
            }
        }

    }
}