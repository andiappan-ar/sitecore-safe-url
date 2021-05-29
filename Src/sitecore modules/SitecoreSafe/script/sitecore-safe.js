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
                    "Domain": "arsc2.dev.local",
                    "Port": 0
                },
                {
                    "Domain": "arsc4.dev.local",
                    "Port": 8446
                }
            ]
        },
        "Modules": {
            "QueryString": {
                "Common": [
                    {
                        "Urls": ["https://arsc.dev.local1/", "https://arsc1.dev.local/"],
                        "ThreatCharacters": "<>",
                        "ThreatPageId": "{76B024D8-0F6A-4A46-B588-E5E6BE275E42}"
                    },
                    {
                        "Urls": ["https://arsc2.dev.local/"],
                        "ThreatCharacters": "<>",
                        "ThreatPageId": "{76B024D8-0F6A-4A46-B588-E5E6BE275E42}"
                    }
                ],
                "AllSites": [
                    {
                        "Url": "https://arsc.dev.local1/",
                        "ThreatCharacters": "*",
                        "ThreatPageId": "{93947600-304A-4935-A49D-199D6AB2D7C9}"
                    },
                    {
                        "Url": "https://xp0cm1.localhost/",
                        "ThreatCharacters": "<>",
                        "ThreatPageId": ""
                    }
                ]
            },
            "SecurityHeader": {
                "Common": [
                    {
                        "Urls": ["https://arsc.dev.local1/", "https://arsc1.dev.local/"],
                        "Headers": [
                            {
                                "HeaderName": "Content-Security-Policy",
                                "HeaderValue": "default-src 'self'; script-src 'nonce-{NONCE}'; img-src www.gstatic.com; frame-src www.google.com; object-src 'none'; base-uri 'none';",

                                "IsAppend": false
                            }
                        ]
                    },
                    {
                        "Urls": ["https://arsc.dev.local1/"],
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
                        "Url": "https://arsc.dev.local1/",
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
                        "Url": "https://xp0cm1.localhost/",
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
                        deleteConfirm: function (args) {
                            
                        },
                        onItemInserting: function (args) {

                            if (!SitecoreSafe.Helpers.IsUniqueURL(args.item, "Domain", "Port", _settings.SitecoreSafeUrl.Configurations.Urls)) {
                                args.cancel = true;
                                alert("Entered Domain should not be empty / Domain & port should not be duplicated.");
                            }
                        },
                        onItemUpdating: function (args) {

                            if (!SitecoreSafe.Helpers.IsUniqueURL(args.item, "Domain", "Port", _settings.SitecoreSafeUrl.Configurations.Urls)) {
                                args.cancel = true;
                                alert("Entered Domain should not be empty / Domain & port should not be duplicated.");
                            }
                        },
                        onItemDeleting: function (args) {
                            debugger;
                            // cancel deletion of the item with 'protected' field
                            if (args.item.protected) {
                                args.cancel = true;
                            }
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