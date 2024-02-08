using System;
using System.Collections.Generic;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegistry
{
    public static class Constants
    {
        public const string ComponentName = "AustralianBusinessRegistry";
        public const string ProviderName = "Australian Business Registry";
        public static readonly Guid ProviderId = Guid.Parse("59d2a815-d947-4e1b-b04c-6b0d89bf565a");

        public struct KeyName
        {
            public const string ApiToken = "apiToken";
            public const string AcceptedEntityType = "acceptedEntityType";
            public const string Abn = "abnKey";

        }

        public static string About { get; set; } = "Australian Business Registry is the official business registry of Australia";
        public static string Icon { get; set; } = "Resources.abr.png";
        public static string Domain { get; set; } = "N/A";

        public static AuthMethods AuthMethods { get; set; } = new AuthMethods
        {
            token = new List<Control>()
            {
                new Control()
                {
                    displayName = "Api Key",
                    type = "input",
                    isRequired = true,
                    name = KeyName.ApiToken
                },
                new Control()
                {
                    displayName = "Accepted Entity Type",
                    type = "input",
                    isRequired = false,
                    name = KeyName.AcceptedEntityType
                },
                new Control()
                {
                    displayName = "ABN",
                    type = "abn",
                    isRequired = false,
                    name = KeyName.Abn
                }
            }
        };

        public static IEnumerable<Control> Properties { get; set; } = new List<Control>()
        {
            // NOTE: Leaving this commented as an example - BF
            //new()
            //{
            //    displayName = "Some Data",
            //    type = "input",
            //    isRequired = true,
            //    name = "someData"
            //}
        };

        public static Guide Guide { get; set; } = null;
        public static IntegrationType IntegrationType { get; set; } = IntegrationType.Enrichment;
    }
}