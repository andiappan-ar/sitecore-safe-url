using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Safe.Common
{
    public static class SitecoreSafeConstant
    {
        /// <summary>
        /// Sitecore constant
        /// </summary>
        public const string IgnoreSites =  "sitecore_safe_sitesToIgnore";
        public const string RootItem = "sitecore_safe_root_item";
        public const string RootItemJsonField = "SitecoreSafeSettingsJson";
        public const string SitecoreMasterDB = "master";
        

        /// <summary>
        /// Log
        /// </summary>
        public const string LogAppender = "SitecoreSafe";
        public const string LogErrorTitle = "Error: ";

        
    }
}