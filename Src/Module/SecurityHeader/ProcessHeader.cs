using Sitecore.Pipelines.HttpRequest;
using Sitecore.Safe.Common.Helper;
using Sitecore.Safe.Logger;
using Sitecore.Safe.Models;
using Sitecore.Safe.Settings;
using System;
using System.Web;

namespace Sitecore.Safe.Security.SecurityHeader
{
    public static class ProcessHeader
    {
       public static bool SetHeaders(HttpRequestArgs args)
        {
            bool result = false;

            try
            {
                Uri currentUrl = HttpContext.Current.Request.Url;

                // Assign header with values
                Action<SitecoreSafeSecurityHeader> setHeader = (header) =>
                {
                    if (header.IsAppend)
                    {
                        var appenddHeader = args.HttpContext.Response.Headers[header.HeaderName];
                        args.HttpContext.Response.Headers[header.HeaderName] =
                        (string.IsNullOrEmpty(appenddHeader)) ? header.HeaderValue : string.Concat(appenddHeader, header.HeaderValue);
                    }
                    else
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
                if (matchedUrlSecurityHeader != null)
                {
                    matchedUrlSecurityHeader.Headers.ForEach(x =>
                    {
                        setHeader(x);
                    });
                }
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
           
        }
    }
}