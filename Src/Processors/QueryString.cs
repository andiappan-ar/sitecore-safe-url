using Sitecore.Data;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Safe.Common.Helper;
using Sitecore.Safe.Models;
using Sitecore.Safe.Security.QueryString;
using System;
using System.Web;

namespace Sitecore.Safe.Processors
{
    public class QueryString : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            // Skip for non site request
            if (Context.Item == null || Context.Database == null)
            {
                return;
            }

            bool isThreat = false;
            string threatPageId = string.Empty;
            Uri currentUrl = HttpContext.Current.Request.Url;

            // Is current url configured in common
            isThreat = (SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common != null &&
                SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common.Urls.Count > 0 &&
                Utility.IsUrlPresentInCollection(HttpContext.Current.Request.Url, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common.Urls) &&
                ProcessUrl.IsThreatUrl(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common.ThreatCharacters)
                );
            threatPageId = (isThreat) ? SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common.ThreatPageId: threatPageId;

            
            if (!isThreat)
            {               
                // Threat against domain
                QueryStringAllSite matchedUrlThreat =
                    (QueryStringAllSite)Utility.GetItemByUrl<QueryStringAllSite>(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.AllSites);

                if(matchedUrlThreat!=null &&
                    ProcessUrl.IsThreatUrl(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common.ThreatCharacters)
                    )
                {
                    isThreat = true;
                    threatPageId = matchedUrlThreat.ThreatPageId;
                }
            }

            // Write response
            if (isThreat)
            {
                var notFoundPage = Context.Database.GetItem(new ID(threatPageId));

                if (notFoundPage == null)
                    return;

                args.ProcessorItem = notFoundPage;
                args.HttpContext.Response.StatusCode = 400;
                Context.Item = notFoundPage;
            }
        }

    }
}