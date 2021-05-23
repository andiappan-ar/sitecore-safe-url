using Sitecore.Safe.Logger;
using System;
using System.Web;

namespace Sitecore.Safe.Security.QueryString
{
    public static class ProcessUrl
    {
        public static bool IsThreatUrl(Uri uri, string threatCharacters)
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
    }
}