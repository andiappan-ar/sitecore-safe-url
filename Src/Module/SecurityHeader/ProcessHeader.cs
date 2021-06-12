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
                if (SitecoreSafeSettingsService.Settings != null)
                {
                    Uri currentUrl = HttpContext.Current.Request.Url;

                    // Assign header with values
                    Action<List<HeaderDetail>> setHeader = (headerList) =>
                    {
                        headerList?.Where(x => !string.IsNullOrEmpty(x.HeaderName)).ToList().ForEach(header =>
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
                        });
                    };

                    // Common
                    if (SitecoreSafeSettingsService.Settings.HeaderSettings.GroupSettings != null)
                    {
                        List<HeaderDetail> matchedUrlCommonHeaders = Utility.GetItemCollectionByUrlCollection<HeaderDetail>
                            (currentUrl, SitecoreSafeSettingsService.Settings.HeaderSettings.GroupSettings).Cast<HeaderDetail>().ToList();

                        setHeader(matchedUrlCommonHeaders);
                    }

                    // Get all settings specific to matched Domain
                    HeaderDetail matchedUrlHeaders = (HeaderDetail)Utility.GetItemByUrl<HeaderDetail>(currentUrl,
                        SitecoreSafeSettingsService.Settings.HeaderSettings.DomainSpecificSettings);
                    setHeader(new List<HeaderDetail>() { matchedUrlHeaders });
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