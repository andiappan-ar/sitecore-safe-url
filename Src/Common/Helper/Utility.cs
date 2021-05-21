using Sitecore.Safe.Logger;
using Sitecore.Safe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Safe.Common.Helper
{
    public static class Utility
    {
        public static bool IsUrlAndPortMatching(Uri sourceUrl, string testUrlString)
        {
            bool result = false;

            try
            {
                Uri testUrl = new Uri(testUrlString);

                result = (
                    string.Equals(sourceUrl.Host, testUrl.Host,StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(sourceUrl.Port.ToString(), testUrl.Port.ToString(), StringComparison.InvariantCultureIgnoreCase)
                    );
            }
            catch(Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
        }

        public static bool IsUrlPresentInCollection(Uri sourceUrl, List<string> urlList)
        {
            bool result = false;

            try
            {
                result =  urlList.Where(x => IsUrlAndPortMatching(sourceUrl, x)).FirstOrDefault() !=null;
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
        }

        public static UrlKeyCollection GetItemByUrl<T>(Uri sourceUrl, List<T> itemList) where T: UrlKeyCollection
        {
            UrlKeyCollection result = null;

            try
            {
                result = itemList.Where(x => IsUrlAndPortMatching(sourceUrl, x.Url)).FirstOrDefault();
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
        }
    }
}