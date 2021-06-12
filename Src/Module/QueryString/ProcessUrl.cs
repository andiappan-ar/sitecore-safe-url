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
        public static bool IsThreatUrl(Uri uri, string patternKey, string patternValue)
        {
            bool result = false;

            try
            {
                result = PatternValidator.IsValidString(HttpUtility.UrlDecode(uri.PathAndQuery.ToLower()), patternKey, patternValue);               
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
                if (SitecoreSafeSettingsService.Settings != null)
                {
                    Uri currentUrl = HttpContext.Current.Request.Url;

                    Action<IQueryStringThreat> setThreat = (threatObject) =>
                    {
                        if (threatObject != null && !string.IsNullOrEmpty(threatObject.ThreatDetails) &&
                        !ProcessUrl.IsThreatUrl(currentUrl,threatObject.MatchingType, threatObject.ThreatDetails))
                        {
                            result.IsThreat = true;
                            result.ThreatPageId = threatObject.ThreatPageId;
                        }
                    };

                    // Is ( current URL configured in group && current URL contains threat character ) 
                    if (SitecoreSafeSettingsService.Settings.UrlThreatSettings.GroupSettings != null 
                        && SitecoreSafeSettingsService.Settings.UrlThreatSettings.GroupSettings.Any())
                    {
                        List<UrlThreat> matchedUrlCommonThreats = Utility.GetItemCollectionByUrlCollection<UrlThreat>(currentUrl,
                            SitecoreSafeSettingsService.Settings.UrlThreatSettings.GroupSettings).Cast<UrlThreat>().ToList();
                        matchedUrlCommonThreats.ForEach(x => { setThreat(x); });
                    }

                    // Skip the domain specific check - If threat available in common
                    if (!result.IsThreat)
                    {
                        // Threat against domain
                        UrlThreat matchedUrlThreat = (UrlThreat)Utility.GetItemByUrl<UrlThreat>(currentUrl,
                            SitecoreSafeSettingsService.Settings.UrlThreatSettings.DomainSpecificSettings);
                        setThreat(matchedUrlThreat);
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