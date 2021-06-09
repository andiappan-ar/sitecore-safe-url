using Sitecore.Safe.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Safe.Common.Helper
{
    public static class SitecoreHelper
    {
        private static List<string> SitesToIgnoreSettings = null;
        static SitecoreHelper()
        {
            SitesToIgnoreSettings = Configuration.Settings.GetSetting(SitecoreSafeConstant.IgnoreSites, string.Empty)
                .ToLowerInvariant()
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList<string>();
        }

        public static bool IsValidSiteForPipeline()
        {
            bool result = false;

            try
            {
                //Request to perform when site is not Equal to sitecore default sites. (EDITED)
                result = (Context.Site!=null && Context.PageMode.IsNormal && !SitesToIgnoreSettings.Contains(Context.Site.Name.ToLowerInvariant()));
                
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
            
        }
    }
}