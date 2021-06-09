var _settings = {
    "Product": {
        "SitecoreVersion": "10.1"
    },
    "SitecoreSafeUrl": {
        "Configs": {
            "Urls": [
                {
                    "Domain": "arsc.dev.local",
                    "Port": 443
                },
                {
                    "Domain": "arsc1.dev.local",
                    "Port": 443
                },
                {
                    "Domain": "arsc2.dev.local",
                    "Port": 8446
                },
                {
                    "Domain": "arsc3.dev.local",
                    "Port": 443
                },
                {
                    "Domain": "arsc4.dev.local",
                    "Port": 443
                },
                {
                    "Domain": "arsc5.dev.local",
                    "Port": 443
                },
                {
                    "Domain": "arsc6.dev.local",
                    "Port": 443
                },
                {
                    "Domain": "arsc7.dev.local",
                    "Port": 443
                },
                {
                    "Domain": "arsc8.dev.local",
                    "Port": 443
                }
            ]
        },
        "Modules": {
            "QueryUrl": {
                "Common": [
                    {
                        "Urls": [0, 1, 3, 4, 7, 8],
                        "ThreatCharacters": "<>",
                        "ThreatPageId": "{76B024D8-0F6A-4A46-B588-E5E6BE275E42}"
                    },
                    {
                        "Urls": [1],
                        "ThreatCharacters": "<>",
                        "ThreatPageId": "{76B024D8-0F6A-4A46-B588-E5E6BE275E42}"
                    }
                ],
                "AllSites": [
                    {
                        "Url": 7,
                        "ThreatCharacters": "*",
                        "ThreatPageId": "{93947600-304A-4935-A49D-199D6AB2D7C9}"
                    },
                    {
                        "Url": 8,
                        "ThreatCharacters": "<>",
                        "ThreatPageId": ""
                    }
                ]
            },
            "SecurityHeader": {
                "Common": [
                    {
                        "Urls": [0, 1, 2],
                        "Headers": [
                            {
                                "HeaderName": "Content-Security-Policy",
                                "HeaderValue": "default-src 'self'; script-src 'nonce-{NONCE}'; img-src www.gstatic.com; frame-src www.google.com; object-src 'none'; base-uri 'none';",

                                "IsAppend": false
                            }
                        ]
                    },
                    {
                        "Urls": [0, 1],
                        "Headers": [
                            {
                                "HeaderName": "X-Frame-Options",
                                "HeaderValue": "SAMEORIGIN1",

                                "IsAppend": false
                            },
                            {
                                "HeaderName": "Strict-Transport-Security",
                                "HeaderValue": "max-age=31536000; includeSubDomains; preload",

                                "IsAppend": false
                            }
                        ]
                    }
                ],
                "AllSites": [
                    {
                        "Url": 0,
                        "Headers": [
                            {
                                "HeaderName": "Content-Security-Policy",
                                "HeaderValue": "child-src 'none';",
                                "IsAppend": true
                            },
                            {
                                "HeaderName": "X-Frame-Options",
                                "HeaderValue": "SAMEORIGIN",
                                "IsAppend": false
                            },
                            {
                                "HeaderName": "X-Content-Type-Options",
                                "HeaderValue": "nosniff",
                                "IsAppend": false
                            }
                        ]
                    },
                    {
                        "Url": 0,
                        "Headers": [
                            {
                                "HeaderName": "value",
                                "HeaderValue": "value"
                            },
                            {
                                "HeaderName": "value",
                                "HeaderValue": "value"
                            }
                        ]
                    }
                ]
            },
            "SafeValidationAttributes": {
                "Regex": [
                    {
                        "key": "AlphaNumeric",
                        "value": "^[A-Z][a-zA-Z]*$",
                        "ErrorMessage": "AlphaNumeric ErrorMessage"
                    }
                ],
                "CharacterMatch": [
                    {
                        "key": "scriptInjection",
                        "value": "<>",
                        "ErrorMessage": "scriptInjection ErrorMessage"
                    },
                    {
                        "key": "sqlInjection",
                        "value": "value",
                        "ErrorMessage": "sqlInjection ErrorMessage"
                    }
                ]

            },
            "Recaptcha": [
                {
                    "Name": "Visible",
                    "Url": "",
                    "ClientKey": "",
                    "SecretKey": ""
                },
                {
                    "Name": "InVisible",
                    "Url": "",
                    "ClientKey": "",
                    "SecretKey": ""
                }
            ]
        }

    }
};


var sfConfig = {
    Const: {
        Domain: "Domain",
        Port: "Port",
        UrlGridId: "#url-config-grid",
        Url: "Urls",
        ThreatCharacters: "ThreatCharacters",
        ThreatPageId: "ThreatPageId",
        MultiSelectUrl:"MultiSelectUrl",
        SettingsUrlCommonGridId: "#url-config-grid",
        SettingsUrlDomainGridId: "#url-group-grid"
    }
};


var SitecoreSafe = (function () {
    return {
        Config: {           
            JsGrid: {
                ConfigUrl: {
                    Id: sfConfig.Const.UrlGridId,
                    GridAttr: {
                        width: "100%",
                        height: "400px",
                        filtering: true,
                        editing: true,
                        sorting: true,
                        inserting: true,
                        paging: true,
                        pageSize: 5,
                        onItemInserting: function (args) {
                            // Is unique check
                            if (!SitecoreSafe.Helpers.IsUniqueURL(args.item, sfConfig.Const.Domain, sfConfig.Const.Port, _settings.SitecoreSafeUrl.Configs.Urls)) {
                                args.cancel = true;
                                alert("Entered Domain or Port should not be empty / Domain & port should not be duplicated.");
                            }
                            _settings.SitecoreSafeUrl.Configs.Urls = $(sfConfig.Const.UrlGridId).jsGrid("option", "data");
                        },
                        onItemUpdating: function (args) {
                            // Is edit made
                            if (args.item.Domain.toLowerCase() === args.previousItem.Domain.toLowerCase() && args.item.Port === args.previousItem.Port) {
                                args.cancel = true;
                                alert("No changes been made.");
                            } else
                                // Is unique check
                                if (!SitecoreSafe.Helpers.IsUniqueURL(args.item, sfConfig.Const.Domain, sfConfig.Const.Port, _settings.SitecoreSafeUrl.Configs.Urls)) {
                                    args.cancel = true;
                                    alert("Entered Domain should not be empty / Domain & port should not be duplicated.");
                                }
                            _settings.SitecoreSafeUrl.Configs.Urls = $(sfConfig.Const.UrlGridId).jsGrid("option", "data");
                        },
                        onItemDeleting: function (args) {
                            var deletedItemIndex = args.itemIndex;
                            var urlKey = sfConfig.Const.Url;                            
                            // Check the item present in other settings
                            if (SitecoreSafe.Helpers.IsUrlIndexPresentInCommonSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.QueryUrl.Common, urlKey)
                                ||
                                SitecoreSafe.Helpers.IsUrlIndexPresentInCommonSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common, urlKey)
                            ) {
                                args.cancel = true;
                                alert("This item cant be delete, its used in common settings.");
                            }
                            else
                                // Check the item present in other settings
                                if (SitecoreSafe.Helpers.IsUrlIndexPresentInDomainSpecificSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites, urlKey)
                                    ||
                                    SitecoreSafe.Helpers.IsUrlIndexPresentInDomainSpecificSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites, urlKey)
                                ) {
                                    args.cancel = true;
                                    alert("This item cant be delete, its used in domain specific settings.");
                                }


                            _settings.SitecoreSafeUrl.Configs.Urls = $(sfConfig.Const.UrlGridId).jsGrid("option", "data");
                        },
                        onItemDeleted: function (args) {
                            var urlKey = sfConfig.Const.Url;
                            var deletedItemIndex = args.itemIndex;
                            // Common settings URL id updation
                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.QueryUrl.Common.length; ++index) {
                                _settings.SitecoreSafeUrl.Modules.QueryUrl.Common[index][urlKey] =
                                    SitecoreSafe.Helpers.GetTheUpdatedUrlIndexAfterDelete(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.QueryUrl.Common[index][urlKey]);
                            }

                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common.length; ++index) {
                                _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common[index][urlKey] =
                                    SitecoreSafe.Helpers.GetTheUpdatedUrlIndexAfterDelete(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common[index][urlKey]);
                            }
                            // Domain specic settings URL id updation
                            urlKey = "Url";
                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites.length; ++index) {
                                var loopedValue = _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites[index][urlKey];
                                _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites[index][urlKey] = (loopedValue > deletedItemIndex) ? loopedValue - 1 : loopedValue;
                            }
                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites.length; ++index) {
                                var loopedValue = _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites[index][urlKey];
                                _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites[index][urlKey] = (loopedValue > deletedItemIndex) ? loopedValue - 1 : loopedValue;
                            }
                        },
                        data: _settings.SitecoreSafeUrl.Configs.Urls,
                        fields: [
                            { name: sfConfig.Const.Domain, type: "text", width: 100 },
                            { name: sfConfig.Const.Port, type: "number", width: 10 },
                            { type: "control", width: 10 }
                        ]
                    }
                },
                SettingsUrlCommon: {
                    Id: sfConfig.Const.SettingsUrlCommonGridId,
                    GridAttr: {
                        width: "100%",
                        height: "400px",
                        filtering: true,
                        editing: true,
                        sorting: true,
                        inserting: true,
                        paging:true,
                        pageSize: 5,
                        onItemInserting: function (args) {
                            // Is unique check
                            if (!SitecoreSafe.Helpers.IsUniqueURL(args.item, sfConfig.Const.Domain, sfConfig.Const.Port, _settings.SitecoreSafeUrl.Configs.Urls)) {
                                args.cancel = true;
                                alert("Entered Domain or Port should not be empty / Domain & port should not be duplicated.");
                            }
                            _settings.SitecoreSafeUrl.Configs.Urls = $(sfConfig.Const.SettingsUrlCommonGridId).jsGrid("option", "data");
                        },
                        onItemUpdating: function (args) {
                            // Is edit made
                            if (args.item.Domain.toLowerCase() === args.previousItem.Domain.toLowerCase() && args.item.Port === args.previousItem.Port) {
                                args.cancel = true;
                                alert("No changes been made.");
                            } else
                                // Is unique check
                                if (!SitecoreSafe.Helpers.IsUniqueURL(args.item, sfConfig.Const.Domain, sfConfig.Const.Port, _settings.SitecoreSafeUrl.Configs.Urls)) {
                                    args.cancel = true;
                                    alert("Entered Domain should not be empty / Domain & port should not be duplicated.");
                                }
                            _settings.SitecoreSafeUrl.Configs.Urls = $(sfConfig.Const.SettingsUrlCommonGridId).jsGrid("option", "data");
                        },
                        onItemDeleting: function (args) {
                            var deletedItemIndex = args.itemIndex;
                            var urlKey = sfConfig.Const.Url;
                            // Check the item present in other settings
                            if (SitecoreSafe.Helpers.IsUrlIndexPresentInCommonSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.QueryUrl.Common, urlKey)
                                ||
                                SitecoreSafe.Helpers.IsUrlIndexPresentInCommonSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common, urlKey)
                            ) {
                                args.cancel = true;
                                alert("This item cant be delete, its used in common settings.");
                            }
                            else
                                // Check the item present in other settings
                                if (SitecoreSafe.Helpers.IsUrlIndexPresentInDomainSpecificSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites, urlKey)
                                    ||
                                    SitecoreSafe.Helpers.IsUrlIndexPresentInDomainSpecificSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites, urlKey)
                                ) {
                                    args.cancel = true;
                                    alert("This item cant be delete, its used in domain specific settings.");
                                }


                            _settings.SitecoreSafeUrl.Configs.Urls = $(sfConfig.Const.SettingsUrlCommonGridId).jsGrid("option", "data");
                        },
                        onItemDeleted: function (args) {
                            var urlKey = sfConfig.Const.Url;
                            var deletedItemIndex = args.itemIndex;
                            // Common settings URL id updation
                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.QueryUrl.Common.length; ++index) {
                                _settings.SitecoreSafeUrl.Modules.QueryUrl.Common[index][urlKey] =
                                    SitecoreSafe.Helpers.GetTheUpdatedUrlIndexAfterDelete(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.QueryUrl.Common[index][urlKey]);
                            }

                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common.length; ++index) {
                                _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common[index][urlKey] =
                                    SitecoreSafe.Helpers.GetTheUpdatedUrlIndexAfterDelete(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common[index][urlKey]);
                            }
                            // Domain specic settings URL id updation
                            urlKey = "Url";
                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites.length; ++index) {
                                var loopedValue = _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites[index][urlKey];
                                _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites[index][urlKey] = (loopedValue > deletedItemIndex) ? loopedValue - 1 : loopedValue;
                            }
                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites.length; ++index) {
                                var loopedValue = _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites[index][urlKey];
                                _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites[index][urlKey] = (loopedValue > deletedItemIndex) ? loopedValue - 1 : loopedValue;
                            }
                        },
                        data: _settings.SitecoreSafeUrl.Modules.QueryUrl.Common,
                        fields: [
                            { name: sfConfig.Const.MultiSelectUrl, type: "multiselect", width: 100, align: "center" },
                            { name: sfConfig.Const.ThreatCharacters, type: "text", width: 50 },
                            { name: sfConfig.Const.ThreatPageId, type: "text", width: 50 },
                            { type: "control", width: 10 }
                        ]
                    }
                },
                SettingsUrlDomain: {
                    Id: sfConfig.Const.UrlGridId,
                    GridAttr: {
                        width: "100%",
                        height: "400px",
                        filtering: true,
                        editing: true,
                        sorting: true,
                        inserting: true,

                        pageSize: 5,
                        onItemInserting: function (args) {
                            // Is unique check
                            if (!SitecoreSafe.Helpers.IsUniqueURL(args.item, sfConfig.Const.Domain, sfConfig.Const.Port, _settings.SitecoreSafeUrl.Configs.Urls)) {
                                args.cancel = true;
                                alert("Entered Domain or Port should not be empty / Domain & port should not be duplicated.");
                            }
                            _settings.SitecoreSafeUrl.Configs.Urls = $(sfConfig.Const.UrlGridId).jsGrid("option", "data");
                        },
                        onItemUpdating: function (args) {
                            // Is edit made
                            if (args.item.Domain.toLowerCase() === args.previousItem.Domain.toLowerCase() && args.item.Port === args.previousItem.Port) {
                                args.cancel = true;
                                alert("No changes been made.");
                            } else
                                // Is unique check
                                if (!SitecoreSafe.Helpers.IsUniqueURL(args.item, sfConfig.Const.Domain, sfConfig.Const.Port, _settings.SitecoreSafeUrl.Configs.Urls)) {
                                    args.cancel = true;
                                    alert("Entered Domain should not be empty / Domain & port should not be duplicated.");
                                }
                            _settings.SitecoreSafeUrl.Configs.Urls = $(sfConfig.Const.UrlGridId).jsGrid("option", "data");
                        },
                        onItemDeleting: function (args) {
                            var deletedItemIndex = args.itemIndex;
                            var urlKey = sfConfig.Const.Url;
                            // Check the item present in other settings
                            if (SitecoreSafe.Helpers.IsUrlIndexPresentInCommonSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.QueryUrl.Common, urlKey)
                                ||
                                SitecoreSafe.Helpers.IsUrlIndexPresentInCommonSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common, urlKey)
                            ) {
                                args.cancel = true;
                                alert("This item cant be delete, its used in common settings.");
                            }
                            else
                                // Check the item present in other settings
                                if (SitecoreSafe.Helpers.IsUrlIndexPresentInDomainSpecificSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites, urlKey)
                                    ||
                                    SitecoreSafe.Helpers.IsUrlIndexPresentInDomainSpecificSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites, urlKey)
                                ) {
                                    args.cancel = true;
                                    alert("This item cant be delete, its used in domain specific settings.");
                                }


                            _settings.SitecoreSafeUrl.Configs.Urls = $(sfConfig.Const.UrlGridId).jsGrid("option", "data");
                        },
                        onItemDeleted: function (args) {
                            var urlKey = sfConfig.Const.Url;
                            var deletedItemIndex = args.itemIndex;
                            // Common settings URL id updation
                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.QueryUrl.Common.length; ++index) {
                                _settings.SitecoreSafeUrl.Modules.QueryUrl.Common[index][urlKey] =
                                    SitecoreSafe.Helpers.GetTheUpdatedUrlIndexAfterDelete(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.QueryUrl.Common[index][urlKey]);
                            }

                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common.length; ++index) {
                                _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common[index][urlKey] =
                                    SitecoreSafe.Helpers.GetTheUpdatedUrlIndexAfterDelete(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common[index][urlKey]);
                            }
                            // Domain specic settings URL id updation
                            urlKey = "Url";
                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites.length; ++index) {
                                var loopedValue = _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites[index][urlKey];
                                _settings.SitecoreSafeUrl.Modules.QueryUrl.AllSites[index][urlKey] = (loopedValue > deletedItemIndex) ? loopedValue - 1 : loopedValue;
                            }
                            for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites.length; ++index) {
                                var loopedValue = _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites[index][urlKey];
                                _settings.SitecoreSafeUrl.Modules.SecurityHeader.AllSites[index][urlKey] = (loopedValue > deletedItemIndex) ? loopedValue - 1 : loopedValue;
                            }
                        },
                        data: _settings.SitecoreSafeUrl.Configs.Urls,
                        fields: [
                            { name: sfConfig.Const.Domain, type: "text", width: 100 },
                            { name: sfConfig.Const.Port, type: "number", width: 10 },
                            { type: "control", width: 50 }
                        ]
                    }
                },
            }
        },
        Init: function () {
            SitecoreSafe.Module.JsGrid.Init();
        },
        Module: {

            JsGrid: {
                Init: function () {
                    SitecoreSafe.Module.JsGrid.InitJsGrid(SitecoreSafe.Config.JsGrid.ConfigUrl);
                    SitecoreSafe.Helpers.InitiateMultiSelectUrl();
                    SitecoreSafe.Module.JsGrid.InitJsGrid(SitecoreSafe.Config.JsGrid.SettingsUrlCommon);
                    SitecoreSafe.Module.JsGrid.InitJsGrid(SitecoreSafe.Config.JsGrid.SettingsUrlDomain);
                },
                InitJsGrid: function (gridConfig) {
                    // Init grid
                    $(gridConfig.Id).jsGrid(gridConfig.GridAttr);
                }
            }
        },
        Helpers: {
            IsUniqueURL: function (args, urlKey, portKey, source) {
                var result = true;
                // Empty check
                if ((args[urlKey] !== null && args[urlKey] !== "" && args[urlKey] !== undefined) &&
                    (args[portKey] !== null && args[portKey] !== "" && args[portKey] !== undefined)) {
                    // Duplicate value check
                    for (var index = 0; index < source.length; ++index) {
                        var loopedObj = source[index];
                        if (loopedObj[urlKey].toLowerCase() === args[urlKey].toLowerCase()
                            && loopedObj[portKey] === args[portKey]) { result = false; break; }
                    }
                }
                else { result = false; }
                return result;
            },
            IsUrlIndexPresentInCommonSettings: function (deletedItemIndex, source, urlKey) {
                var result = false;

                for (var index = 0; index < source.length; ++index) {
                    var loopedObj = source[index];
                    if (loopedObj[urlKey].indexOf(deletedItemIndex) !== -1) { result = true; break; }
                }

                return result;
            },
            IsUrlIndexPresentInDomainSpecificSettings: function (deletedItemIndex, source, urlKey) {
                var result = false;

                // Check the item present in domain specific settings
                for (var index = 0; index < source.length; ++index) {
                    var loopedObj = source[index];
                    if (loopedObj[urlKey] === deletedItemIndex) { result = true; break; }
                }

                return result;
            },
            GetTheUpdatedUrlIndexAfterDelete: function (deletedItemIndex, source) {
                var result = [];

                for (var index = 0; index < source.length; ++index) {
                   
                    if (source[index] > deletedItemIndex) {
                        result.push(source[index]-1);
                    }
                    else if (source[index] !== deletedItemIndex)
                    {
                        result.push(source[index]);
                    }
                }
                               
                return result;
            },
            InitiateMultiSelectUrl: function () {
                var multiSelect = [];

                $(_settings.SitecoreSafeUrl.Configs.Urls).each(function (index) {
                    var tempItem = { Name: this.Domain + ":" + this.Port, Value: index };
                    multiSelect.push(tempItem);
                });

                // Assign multi select value
                for (var index = 0; index < _settings.SitecoreSafeUrl.Modules.QueryUrl.Common.length; ++index) {
                    var copiedObject = jQuery.extend(true, {}, multiSelect);
                    var sourceIndex = _settings.SitecoreSafeUrl.Modules.QueryUrl.Common[index];
                    $(copiedObject).each(function (indexInner) {
                        // Make select if its available
                        copiedObject[indexInner].Selected =
                            (sourceIndex.Urls.indexOf(indexInner) !== -1)? true:false;
                       
                    });
                }               

            }
        },
        EventHalndler: {
            Init: function () {

            },

        },
        Extention: function () {
          
        }
    }

})();

$(document).ready(function () {
    SitecoreSafe.Extention();
    var MultiselectField = function (config) {
        jsGrid.Field.call(this, config);
    };

    MultiselectField.prototype = new jsGrid.Field({

        items: [],
        textField: "",

        itemTemplate: function (value) {
            return $.makeArray(value).join(", ");
        },

        _createSelect: function (selected) {
            var textField = this.textField;
            var $result = $("<select>").prop("multiple", true);

            var _indx = 0;
            $.each(this.items, function (_, item) {
                var value = item[textField];
                var $opt = $("<option>").text(value);
                $opt.attr("value", _indx);
                if ($.inArray(value, selected) > -1) {
                    $opt.attr("selected", "selected");
                }

                $result.append($opt);
                _indx++;
            });

            return $result;
        },

        insertTemplate: function () {
            var insertControl = this._insertControl = this._createSelect();

            setTimeout(function () {
                insertControl.multiselect({
                    minWidth: 140
                });
            });

            return insertControl;
        },

        editTemplate: function (value) {
            var editControl = this._editControl = this._createSelect(value);

            setTimeout(function () {
                editControl.multiselect({
                    minWidth: 140
                });
            });

            return editControl;
        },

        insertValue: function () {
            return this._insertControl.find("option:selected").map(function () {
                return this.selected ? $(this).text() : null;
            });
        },

        editValue: function () {
            return this._editControl.find("option:selected").map(function () {
                return this.selected ? $(this).text() : null;
            });
        }

    });

    jsGrid.fields.multiselect = MultiselectField;
    SitecoreSafe.Init();
});