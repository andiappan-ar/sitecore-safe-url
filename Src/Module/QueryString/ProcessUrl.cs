using Sitecore.Safe.Common.Helper;
using Sitecore.Safe.Logger;
using Sitecore.Safe.Models;
using Sitecore.Safe.Models.Module;
using Sitecore.Safe.Settings;
using System;
using System.Web;

namespace Sitecore.Safe.Security.QueryString
{
    public static class ProcessUrl
    {
        public static bool IsUrlConfigured(Uri uri, string threatCharacters)
        {
            bool result = false;

            try
            {
                foreach (char @char in HttpUtility.UrlDecode(uri.PathAndQuery.ToLower()))
                {
                    if (threatCharacters.IndexOf(@char) != -1)
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
        }

        public static QueryStringThreat GetQueryStringThreat()
        {
            QueryStringThreat result = new QueryStringThreat {
                IsThreat = false,
                ThreatPageId = string.Empty
            };

            try
            {
                
                Uri currentUrl = HttpContext.Current.Request.Url;

                // Is current url configured in common
                result.IsThreat = (SitecoreSafeSettings.JsonSettings != null && SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common != null &&
                    SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common.Urls.Count > 0 &&
                    Utility.IsUrlPresentInCollection(HttpContext.Current.Request.Url, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common.Urls) &&
                    ProcessUrl.IsUrlConfigured(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common.ThreatCharacters)
                    );
                result.ThreatPageId = (result.IsThreat) ? SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common.ThreatPageId : result.ThreatPageId;


                if (!result.IsThreat)
                {
                    // Threat against domain
                    QueryStringAllSite matchedUrlThreat =
                        (QueryStringAllSite)Utility.GetItemByUrl<QueryStringAllSite>(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.AllSites);

                    if (matchedUrlThreat != null &&
                        ProcessUrl.IsUrlConfigured(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common.ThreatCharacters)
                        )
                    {
                        result.IsThreat = true;
                        result.ThreatPageId = matchedUrlThreat.ThreatPageId;
                    }
                }
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
                result = null;
            }

            return result;
        }
    }
}