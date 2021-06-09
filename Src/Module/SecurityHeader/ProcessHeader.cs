using Sitecore.Pipelines.HttpRequest;
using Sitecore.Safe.Common.Helper;
using Sitecore.Safe.Logger;
using Sitecore.Safe.Models;
using Sitecore.Safe.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
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
                //if (SitecoreSafeSettings.JsonSettings != null)
                //{
                //    Uri currentUrl = HttpContext.Current.Request.Url;

                //    // Assign header with values
                //    Action<List<SitecoreSafeSecurityHeader>> setHeader = (headerList) =>
                //    {                        
                //            headerList?.Where(x => !string.IsNullOrEmpty(x.HeaderName)).ToList().ForEach(header =>
                //            {
                //                if (header.IsAppend)
                //                {
                //                    var appenddHeader = args.HttpContext.Response.Headers[header.HeaderName];
                //                    args.HttpContext.Response.Headers[header.HeaderName] =
                //                    (string.IsNullOrEmpty(appenddHeader)) ? header.HeaderValue : string.Concat(appenddHeader, header.HeaderValue);
                //                }
                //                else
                //                {
                //                    args.HttpContext.Response.Headers[header.HeaderName] = header.HeaderValue;
                //                }
                //            });
                //    };                    

                //    // Common
                //    if (SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.SecurityHeader.Common != null)
                //    {
                //        List<SecurityHeaderCommon> matchedUrlCommonHeaders = Utility.GetItemCollectionByUrlCollection<SecurityHeaderCommon>(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.SecurityHeader.Common).Cast<SecurityHeaderCommon>().ToList();
                //        matchedUrlCommonHeaders.ForEach(x => {setHeader(x.Headers);});
                //    }                    

                //    // Get all settings specific to matched Domain
                //    SecurityHeaderAllSite matchedUrlHeaders = (SecurityHeaderAllSite)Utility.GetItemByUrl<SecurityHeaderAllSite>(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites);
                //    setHeader(matchedUrlHeaders.Headers);                    
                //}
                
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
           
        }
    }
}