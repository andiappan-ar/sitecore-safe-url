using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Safe.Common;
using Sitecore.Safe.Logger;
using Sitecore.Safe.Models;
using System;

namespace Sitecore.Safe.Settings
{
    public static class SitecoreSafeSettings
    {
        public static SitecoreSafeRoot JsonSettings = null;
        static SitecoreSafeSettings()
        {
            JsonSettings = GetSitecoreSafeRoot();
        }

        public static SitecoreSafeRoot GetSitecoreSafeRoot()
        {
            SitecoreSafeRoot result = null;

            try
            {
                var rootItemKey = Sitecore.Configuration.Settings.GetSetting(SitecoreSafeConstant.RootItem);
                Item rootItem = GetSafeDataBase().GetItem(rootItemKey);

                if (rootItem!=null)
                {
                    string settingsJson = rootItem[SitecoreSafeConstant.RootItemJsonField];
                    result = JsonConvert.DeserializeObject<SitecoreSafeRoot>(settingsJson);
                }
            }
            catch(Exception error)
            {
                SitecoreSafeLog.Error(error);
            }

            return result;
        }

        private static Database GetSafeDataBase()
        {
            Database result = null;

            try
            {
                result = (Sitecore.Context.Database != null) ? Sitecore.Context.Database : Sitecore.Data.Database.GetDatabase(SitecoreSafeConstant.SitecoreMasterDB);
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
                result = Sitecore.Data.Database.GetDatabase(SitecoreSafeConstant.SitecoreMasterDB);
            }

            return result;
        }
    }
}