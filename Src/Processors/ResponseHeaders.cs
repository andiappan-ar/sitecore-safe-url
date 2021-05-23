using Sitecore.Pipelines.HttpRequest;
using Sitecore;
using Sitecore.Data;
using Sitecore.Safe.Common.Helper;
using System;
using System.Web;
using Sitecore.Safe.Models;

namespace Sitecore.Safe.Processors
{
    public class ResponseHeaders : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (!SitecoreHelper.IsValidSiteForPipeline())
            {
                return;
            }

            Uri currentUrl = HttpContext.Current.Request.Url;

            // Assign header with values
            Action<SitecoreSafeSecurityHeader> setHeader = (header) =>
            {               
                if(header.IsAppend)
                {
                    var appenddHeader = args.HttpContext.Response.Headers[header.HeaderName];
                    args.HttpContext.Response.Headers[header.HeaderName] = 
                    (string.IsNullOrEmpty(appenddHeader))? header.HeaderValue : string.Concat(appenddHeader, header.HeaderValue);
                }else
                {
                    args.HttpContext.Response.Headers[header.HeaderName] = header.HeaderValue;
                }

                

            };

            // Common
            if (Utility.IsUrlPresentInCollection(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.SecurityHeader.Common.Urls))
            {
                SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.SecurityHeader.Common.Headers.ForEach(x =>
                {
                    setHeader(x);
                });
            }

            // Get all settings specific to matched Domain

            SecurityHeaderAllSite matchedUrlSecurityHeader =
                   (SecurityHeaderAllSite)Utility.GetItemByUrl<SecurityHeaderAllSite>(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites);
            if (matchedUrlSecurityHeader!=null)
            {
                matchedUrlSecurityHeader.Headers.ForEach(x =>
                {
                    setHeader(x);
                });
            }

        }
    }
}