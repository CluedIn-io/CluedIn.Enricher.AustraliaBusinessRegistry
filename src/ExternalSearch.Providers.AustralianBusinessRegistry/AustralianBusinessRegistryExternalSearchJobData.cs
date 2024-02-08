using System.Collections.Generic;
using CluedIn.Core.Crawling;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegistry
{
    public class AustralianBusinessRegistryExternalSearchJobData : CrawlJobData
    {
        public AustralianBusinessRegistryExternalSearchJobData(IDictionary<string, object> configuration)
        {
            ApiToken = GetValue<string>(configuration, Constants.KeyName.ApiToken);
            AcceptedEntityType = GetValue<string>(configuration, Constants.KeyName.AcceptedEntityType);
            OrgNameKey = GetValue<string>(configuration, Constants.KeyName.Abn);

        }

        public IDictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object> {
                { Constants.KeyName.ApiToken, ApiToken },
                { Constants.KeyName.AcceptedEntityType, AcceptedEntityType },
                { Constants.KeyName.Abn, OrgNameKey }
            };
        }

        public string ApiToken { get; set; }
        public string AcceptedEntityType { get; set; }
        public string OrgNameKey { get; set; }

    }
}
