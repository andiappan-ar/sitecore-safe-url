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
        public static bool IsUrlAndPortMatching(Uri sourceUrl, string domainAndPort)
        {
            bool result = false;

            try
            {
                string[] urlInfo = domainAndPort.Split(':');
                if (urlInfo.Length > 1)
                {
                    result = (
                    string.Equals(sourceUrl.Host, urlInfo.FirstOrDefault(), StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(sourceUrl.Port.ToString(), urlInfo.LastOrDefault(), StringComparison.InvariantCultureIgnoreCase)
                    );
                }
                
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

        public static IUrlKeyCollection GetItemByUrl<T>(Uri sourceUrl, List<T> itemList) where T: IUrlKeyCollection
        {
            IUrlKeyCollection result = null;

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

        public static List<IUrlListKeyCollection> GetItemCollectionByUrlCollection<T>(Uri sourceUrl, List<T> itemList) where T : IUrlListKeyCollection
        {
            List<IUrlListKeyCollection> result = new List<IUrlListKeyCollection>();

            try
            {
                itemList.ForEach(urlGroup=> {
                    var isUrlMatched = urlGroup.Urls.Any(x => IsUrlAndPortMatching(sourceUrl, x.DomainAndPort));
                    if (isUrlMatched)
                    {
                        result.Add(urlGroup);
                    }                    
                });
                
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
        }
    }
}