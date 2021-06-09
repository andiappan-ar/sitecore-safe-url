using Sitecore.Safe.Common.Helper;
using Sitecore.Safe.Logger;
using Sitecore.Safe.Models;
using Sitecore.Safe.Models.Module;
using Sitecore.Safe.Settings;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;

namespace Sitecore.Safe.Security.QueryString
{
    public static class ProcessUrl
    {
        public static bool IsUrlContainsThreat(Uri uri, string threatCharacters)
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
                //if(SitecoreSafeSettings.JsonSettings != null)
                //{
                //    Uri currentUrl = HttpContext.Current.Request.Url;

                //    Action<IQueryStringThreat> setThreat = (threatObject) => {
                //        if (threatObject != null && !string.IsNullOrEmpty(threatObject.ThreatCharacters) && ProcessUrl.IsUrlContainsThreat(currentUrl, threatObject.ThreatCharacters))
                //        {
                //            result.IsThreat = true;
                //            result.ThreatPageId = threatObject.ThreatPageId;
                //        }
                //    };

                //    // Is ( current URL configured in common && current URL contains threat character ) 
                //    if (SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common != null)
                //    {                       
                //        List<QueryStringCommon> matchedUrlCommonThreats = Utility.GetItemCollectionByUrlCollection<QueryStringCommon>(currentUrl,SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.Common).Cast<QueryStringCommon>().ToList();
                //        matchedUrlCommonThreats.ForEach(x=> {setThreat(x);});
                //    }

                //    // Skip the domain specific check - If threat available in common
                //    if (!result.IsThreat)
                //    {
                //        // Threat against domain
                //        QueryStringAllSite matchedUrlThreat = (QueryStringAllSite)Utility.GetItemByUrl<QueryStringAllSite>(currentUrl, SitecoreSafeSettings.JsonSettings.SitecoreSafeUrl.Modules.QueryString.AllSites);
                //        setThreat(matchedUrlThreat);
                //    }
                //}

                
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