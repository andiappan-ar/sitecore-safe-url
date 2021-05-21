using Sitecore.Safe.Logger;
using System;

namespace Sitecore.Safe.Security.QueryString
{
    public static class ProcessUrl
    {
        public static bool IsThreatUrl(Uri uri, string threatCharacters)
        {
            bool result = false;

            try
            {
                foreach (char @char in uri.PathAndQuery.ToLower())
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