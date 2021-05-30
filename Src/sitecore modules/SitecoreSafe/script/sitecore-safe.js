var _settings = {
    "Product": {
        "SitecoreVersion": "10.1"
    },
    "SitecoreSafeUrl": {
        "Configurations": {
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
                    "Domain": "arsc.dev.local",
                    "Port": 8446
                }
            ]
        },
        "Modules": {
            "QueryString": {
                "Common": [
                    {
                        "Urls": [0,1],
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
                        "Url":0,
                        "ThreatCharacters": "*",
                        "ThreatPageId": "{93947600-304A-4935-A49D-199D6AB2D7C9}"
                    },
                    {
                        "Url":0,
                        "ThreatCharacters": "<>",
                        "ThreatPageId": ""
                    }
                ]
            },
            "SecurityHeader": {
                "Common": [
                    {
                        "Urls": [0,1,2],
                        "Headers": [
                            {
                                "HeaderName": "Content-Security-Policy",
                                "HeaderValue": "default-src 'self'; script-src 'nonce-{NONCE}'; img-src www.gstatic.com; frame-src www.google.com; object-src 'none'; base-uri 'none';",

                                "IsAppend": false
                            }
                        ]
                    },
                    {
                        "Urls": [0,1],
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


var SitecoreSafe = (function () {
    return {
        Configuration: {
            JsGrid: {
                GridConfigurationUrl: {
                    Id: "#url-config-grid",                    
                    GridAttr: {
                        width: "100%",
                        height: "400px",
                        filtering: true,
                        editing: true,
                        sorting: true,
                        inserting:true,
                        paging: true,
                        pageSize:5,                        
                        onItemInserting: function (args) {                            
                            // Is unique check
                            if (!SitecoreSafe.Helpers.IsUniqueURL(args.item, "Domain", "Port", _settings.SitecoreSafeUrl.Configurations.Urls)) {
                                args.cancel = true;
                                alert("Entered Domain should not be empty / Domain & port should not be duplicated.");
                            }
                            _settings.SitecoreSafeUrl.Configurations.Urls = $("#url-config-grid").jsGrid("option", "data");
                        },
                        onItemUpdating: function (args) {
                            // Is edit made
                            if (args.item.Domain.toLowerCase() === args.previousItem.Domain.toLowerCase() && args.item.Port === args.previousItem.Port) {
                                args.cancel = true;
                                alert("No changes been made.");
                            } else
                            // Is unique check
                            if (!SitecoreSafe.Helpers.IsUniqueURL(args.item, "Domain", "Port", _settings.SitecoreSafeUrl.Configurations.Urls)) {
                                args.cancel = true;
                                alert("Entered Domain should not be empty / Domain & port should not be duplicated.");
                                }
                            _settings.SitecoreSafeUrl.Configurations.Urls = $("#url-config-grid").jsGrid("option", "data");
                        },
                        onItemDeleting: function (args) {                            
                            var deletedItemIndex = args.itemIndex;
                            var urlKey = "Urls";
                            // Check the item present in other settings
                            if (SitecoreSafe.Helpers.IsUrlIndexPresentInCommonSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.QueryString.Common, urlKey)
                                ||
                                SitecoreSafe.Helpers.IsUrlIndexPresentInCommonSettings(deletedItemIndex, _settings.SitecoreSafeUrl.Modules.SecurityHeader.Common, urlKey)
                            ) {
                                args.cancel = true;
                                alert("This item cant be delete, its used in common settings.");
                            }

                            _settings.SitecoreSafeUrl.Configurations.Urls = $("#url-config-grid").jsGrid("option", "data");
                        },
                        data: _settings.SitecoreSafeUrl.Configurations.Urls,
                        fields: [
                            { name: "Domain", type: "text", width: 100 },
                            { name: "Port", type: "number", width: 10 },
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
                    SitecoreSafe.Module.JsGrid.InitJsGrid(SitecoreSafe.Configuration.JsGrid.GridConfigurationUrl);
                },
                InitJsGrid: function (gridConfiguration) {
                    // Init grid
                    $(gridConfiguration.Id).jsGrid(gridConfiguration.GridAttr);                    
                }
            }
        },
        Helpers: {
            IsUniqueURL: function (args, urlKey, portKey ,source) {
                var result = true;
                // Empty check
                if ((args[urlKey] !== null && args[urlKey] !== "" && args[urlKey] !== undefined) &&
                    (args[portKey] !== null && args[portKey] !== "" && args[portKey] !== undefined))                    {
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
            IsUrlIndexPresentInCommonSettings: function (deletedItemIndex,source, urlKey) {
                var result = false;
               
                for (var index = 0; index < source.length; ++index) {
                    var loopedObj = source[index];
                    if (loopedObj[urlKey].indexOf(deletedItemIndex) !== -1) { result = true; break; }
                }

                return result;
            }
        },
        EventHalndler: {
            Init: function () {
               
            },
          
        }
    }

})();

$(document).ready(function () {
    SitecoreSafe.Init();
});