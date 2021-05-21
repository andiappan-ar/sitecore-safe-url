using log4net;
using Sitecore.Safe.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Safe.Logger
{
    public static class SitecoreSafeLog
    {
        public static ILog Logger = null;

        static SitecoreSafeLog()
        {
            Logger = Sitecore.Diagnostics.LoggerFactory.GetLogger(SitecoreSafeConstant.LogAppender);
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Error(Exception error)
        {
            Logger.Error(SitecoreSafeConstant.LogErrorTitle, error);
        }
    }
}