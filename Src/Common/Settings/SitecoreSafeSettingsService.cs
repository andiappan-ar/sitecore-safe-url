using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Safe.Common;
using Sitecore.Safe.Logger;
using Sitecore.Safe.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Safe.Settings
{
    public static class SitecoreSafeSettingsService
    {
        public static Models.SitecoreSafeSettings Settings = null;
        static SitecoreSafeSettingsService()
        {
            InitSettings();
        }

        public static void InitSettings()
        {
            Settings = GetSitecoreSafeRoot();
        }

        public static Models.SitecoreSafeSettings GetSitecoreSafeRoot()
        {
            Models.SitecoreSafeSettings result = null;

            try
            {
                var rootItemKey = Configuration.Settings.GetSetting(SitecoreSafeConstant.RootItem);
                Item rootItem = GetSafeDataBase().GetItem(rootItemKey);

                if (rootItem != null)
                {
                    result = SerializeItemToModel(rootItem);
                }
            }
            catch (Exception error)
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
                result = (Context.Database != null) ? Context.Database : Data.Database.GetDatabase(SitecoreSafeConstant.SitecoreMasterDB);
            }
            catch (Exception error)
            {
                SitecoreSafeLog.Error(error);
                result = Data.Database.GetDatabase(SitecoreSafeConstant.SitecoreMasterDB);
            }

            return result;
        }

        private static SitecoreSafeSettings SerializeItemToModel(Item settings)
        {
            SitecoreSafeSettings result = null;

            try
            {
                if (settings != null)
                {
                    Func<Item, string, Item> getChildItemByName = (itm, nme) =>
                    {
                        if(itm!=null && itm.Children!=null && itm.Children.Any())
                        {
                          return  itm.Children.Where(x => x.Name.Equals(nme, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                        }
                        
                        return null;
                    };

                    Func<Item,string,string, string> getTargetLinkStringFieldValue = (itm,refrenceField,targetLinkField) =>
                    {
                        Data.Fields.ReferenceField refr = itm.Fields[refrenceField];                       

                        if (refr != null && refr.TargetItem != null)
                        {
                            return refr.TargetItem.Fields[targetLinkField]?.Value;
                        }

                        return string.Empty;
                    };

                    Func<Item, string,string> getLinkFieldTargetGuid = (itm, fieldName) =>
                    {
                        Data.Fields.LinkField linkField = itm.Fields[fieldName];

                        return (linkField != null && linkField.TargetItem != null)? linkField.TargetID.ToString():string.Empty;
                    };

                    Func<Item, string, string, List<Url>> getUrlsFromMultiList = (itm, refrenceField, targetField) =>
                    {
                        //Read the Multifield List
                        Data.Fields.MultilistField multiselectField = itm.Fields[refrenceField];
                        Data.Items.Item[] items = multiselectField.GetItems();

                        if(items!=null && items.Any())
                        {
                            return items.Where(x => x != null).Select(x => new Url {
                            DomainAndPort = x.Fields[targetField]?.Value
                            }).ToList();
                        }

                        return null;
                    };

                    Func<Item, string, bool> getCheckBoxFieldValue = (itm, fieldName) =>
                    {
                        Data.Fields.CheckboxField checkboxField = itm.Fields[fieldName];

                        return (checkboxField != null) ? checkboxField.Checked : false;                        
                    };

                    result = new SitecoreSafeSettings();
                    // Bind configurations 
                    Item configuration = getChildItemByName(settings, "Configuration");
                    if (configuration!=null)
                    {
                        result.Configuration = new Models.Configuration() { };

                        // PatternType
                        Item patternType = getChildItemByName(configuration, "PatternType");
                        result.Configuration.PatternList = (patternType != null) ?
                            patternType.Children.Select(x => new Pattern
                            {
                                key = x.Fields["key"]?.Value,
                                value = x.Fields["value"]?.Value,
                                ErrorMessage = x.Fields["ErrorMessage"]?.Value,
                                MatchingType = getTargetLinkStringFieldValue(x, "MatchingType", "key"),
                            }).ToList()
                            :
                            null;

                        // Url lists
                        Item urlList = getChildItemByName(configuration, "Url List");
                        result.Configuration.UrlList = (urlList != null) ?
                            urlList.Children.Select(x => new Url
                            {
                                DomainAndPort = x.Fields["DomainAndPort"]?.Value                               
                            }).ToList()
                            :
                            null;
                    }

                    //Bind Url Threat settings
                    Item urlThreatSettings = getChildItemByName(settings, "Url threat settings");
                    if (urlThreatSettings != null)
                    {
                        result.UrlThreatSettings = new Models.UrlThreatSettings() { };

                        // GroupSettings
                        Item groupSettings = getChildItemByName(urlThreatSettings, "Group settings");
                        result.UrlThreatSettings.GroupSettings = (groupSettings != null) ?
                            groupSettings.Children.Select(x => new UrlThreat
                            {
                                ThreatDetails = x.Fields["ThreatDetails"]?.Value,
                                ThreatPageId = getLinkFieldTargetGuid(x, "ThreatPageId"),                                
                                MatchingType = getTargetLinkStringFieldValue(x, "MatchingType", "key"),
                                Urls = getUrlsFromMultiList(x, "Urls", "DomainAndPort")
                            }).ToList()
                            :
                            null;

                        // DomainSpecific settings
                        Item domainSpecificSettings = getChildItemByName(urlThreatSettings, "Group settings");
                        result.UrlThreatSettings.DomainSpecificSettings = (domainSpecificSettings != null) ?
                            domainSpecificSettings.Children.Select(x => new UrlThreat
                            {
                                ThreatDetails = x.Fields["ThreatDetails"]?.Value,
                                ThreatPageId = getLinkFieldTargetGuid(x, "ThreatPageId"),
                                MatchingType = getTargetLinkStringFieldValue(x, "MatchingType", "key"),
                                Url = getTargetLinkStringFieldValue(x, "Url", "DomainAndPort")
                            }).ToList()
                            :
                            null;
                    }

                    //Bind Url Threat settings
                    Item headerSettings = getChildItemByName(settings, "Headers settings");
                    if (headerSettings != null)
                    {
                        result.HeaderSettings = new Models.HeaderSettings() { };

                        // GroupSettings
                        Item groupSettings = getChildItemByName(headerSettings, "Group settings");
                        result.HeaderSettings.GroupSettings = (groupSettings != null) ?
                            groupSettings.Children.Select(x => new HeaderDetail
                            {
                                HeaderName = x.Fields["HeaderName"]?.Value,
                                HeaderValue = x.Fields["HeaderName"]?.Value,
                                IsAppend = getCheckBoxFieldValue(x, "IsAppend"),
                                Urls = getUrlsFromMultiList(x, "Urls", "DomainAndPort")
                            }).ToList()
                            :
                            null;

                        // DomainSpecific settings
                        Item domainSpecificSettings = getChildItemByName(headerSettings, "Group settings");
                        result.HeaderSettings.DomainSpecificSettings = (domainSpecificSettings != null) ?
                            domainSpecificSettings.Children.Select(x => new HeaderDetail
                            {
                                HeaderName = x.Fields["HeaderName"]?.Value,
                                HeaderValue = x.Fields["HeaderName"]?.Value,
                                IsAppend = getCheckBoxFieldValue(x, "IsAppend"),
                                Url = getTargetLinkStringFieldValue(x, "Url", "DomainAndPort")
                            }).ToList()
                            :
                            null;
                    }

                    //Bind Url Threat settings
                    Item validationAttributes = getChildItemByName(settings, "Validation Attributes");
                    if (validationAttributes != null)
                    {
                        
                        result.ValidationSettings.AllSettings = (validationAttributes != null) ?
                            validationAttributes.Children.Select(x => new Pattern
                            {
                                key = x.Fields["key"]?.Value,
                                value = x.Fields["value"]?.Value,
                                ErrorMessage = x.Fields["ErrorMessage"]?.Value,
                                MatchingType = getTargetLinkStringFieldValue(x, "MatchingType", "key"),
                            }).ToList()
                            :
                            null;
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