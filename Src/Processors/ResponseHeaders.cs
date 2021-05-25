using Sitecore.Pipelines.HttpRequest;
using Sitecore.Safe.Common.Helper;
using Sitecore.Safe.Models;
using Sitecore.Safe.Security.SecurityHeader;
using Sitecore.Safe.Settings;
using System;
using System.Web;

namespace Sitecore.Safe.Processors
{
    public class ResponseHeaders : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (SitecoreHelper.IsValidSiteForPipeline())
            {
                ProcessHeader.SetHeaders(args);
            }
        }
    }
}