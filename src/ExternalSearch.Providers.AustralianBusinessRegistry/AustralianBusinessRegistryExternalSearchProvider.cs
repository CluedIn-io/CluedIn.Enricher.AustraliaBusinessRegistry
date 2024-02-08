using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.ExternalSearch;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;
using EntityType = CluedIn.Core.Data.EntityType;
using CluedIn.Core.Data.Vocabularies;
using CluedIn.ExternalSearch.Providers.AustralianBusinessRegistry.Models;
using RestSharp.Deserializers;
using CluedIn.ExternalSearch.Providers.AustralianBusinessRegistry.Vocabularies;
using CluedIn.Crawling.Helpers;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegistry
{
    /// <summary>The googlemaps graph external search provider.</summary>
    /// <seealso cref="CluedIn.ExternalSearch.ExternalSearchProviderBase" />
    public class AustralianBusinessRegistryExternalSearchProvider : ExternalSearchProviderBase, IExtendedEnricherMetadata, IConfigurableExternalSearchProvider
    {
        private static EntityType[] AcceptedEntityTypes = { EntityType.Organization };

        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/

        public AustralianBusinessRegistryExternalSearchProvider()
            : base(Constants.ProviderId, AcceptedEntityTypes)
        {
            var nameBasedTokenProvider = new NameBasedTokenProvider("AustralianBusinessRegistry");

            if (nameBasedTokenProvider.ApiToken != null)
                this.TokenProvider = new RoundRobinTokenProvider(nameBasedTokenProvider.ApiToken.Split(',', ';'));
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        /// <summary>Builds the queries.</summary>
        /// <param name="context">The context.</param>
        /// <param name="request">The request.</param>
        /// <returns>The search queries.</returns>
        public override IEnumerable<IExternalSearchQuery> BuildQueries(ExecutionContext context, IExternalSearchRequest request)
        {
            foreach (var externalSearchQuery in InternalBuildQueries(context, request))
            {
                yield return externalSearchQuery;
            }
        }
        private IEnumerable<IExternalSearchQuery> InternalBuildQueries(ExecutionContext context, IExternalSearchRequest request, IDictionary<string, object> config = null)
        {
            if (config.TryGetValue(Constants.KeyName.AcceptedEntityType, out var customType) && !string.IsNullOrWhiteSpace(customType?.ToString()))
            {
                if (!request.EntityMetaData.EntityType.Is(customType.ToString()))
                {
                    yield break;
                }
            }
            else if (!this.Accepts(request.EntityMetaData.EntityType))
                yield break;

            var existingResults = request.GetQueryResults<ABRPayloadSearchResults>(this).ToList();
           
            var entityType = request.EntityMetaData.EntityType;

            var organizationName = GetValue(request, config, Constants.KeyName.Abn, Core.Data.Vocabularies.Vocabularies.CluedInOrganization.CodesCompanyNumber);

            if (organizationName != null && organizationName.Count > 0)
            {
                foreach (var nameValue in organizationName)
                {
                    var companyDict = new Dictionary<string, string>
                                    {
                                        {"abn", nameValue }
                                    };
                    yield return new ExternalSearchQuery(this, entityType, companyDict);
                }
            }
           
        }

        private static HashSet<string> GetValue(IExternalSearchRequest request, IDictionary<string, object> config, string keyName, VocabularyKey defaultKey)
        {
            HashSet<string> value;
            if (config.TryGetValue(keyName, out var customVocabKey) && !string.IsNullOrWhiteSpace(customVocabKey?.ToString()))
            {
                value = request.QueryParameters.GetValue<string, HashSet<string>>(customVocabKey.ToString(), new HashSet<string>());
            }
            else
            {
                value = request.QueryParameters.GetValue(defaultKey, new HashSet<string>());
            }

            return value;
        }

        /// <summary>Executes the search.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <returns>The results.</returns>
        public override IEnumerable<IExternalSearchQueryResult> ExecuteSearch(ExecutionContext context, IExternalSearchQuery query)
        {
            var apiKey = this.TokenProvider.ApiToken;

            foreach (var externalSearchQueryResult in InternalExecuteSearch(query, apiKey)) yield return externalSearchQueryResult;
        }

        private static IEnumerable<IExternalSearchQueryResult> InternalExecuteSearch(IExternalSearchQuery query, string apiKey)
        {
            var client = new RestClient("https://abr.business.gov.au/abrxmlsearch/AbrXmlSearch.asmx");
            client.UseXml();
            if (query.QueryParameters.ContainsKey("abn"))
            {
                var name = query.QueryParameters["abn"].FirstOrDefault();
                var key = apiKey;

                var requests = new RestRequest($"SearchByABNv202001?searchString={name}&includeHistoricalDetails=Y&authenticationGuid={key}");
                requests.RequestFormat = DataFormat.Xml;
                requests.RootElement = "ABRPayloadSearchResults";
                requests.AddHeader("Host", "abr.business.gov.au");
                requests.AddHeader("Content-Type", "text/xml; charset=utf-8");
                requests.AddHeader("SOAPAction", "http://abr.business.gov.au/ABRXMLSearch/SearchByABNv202001");

                var response = client.Execute(requests);

                var dotNetXmlDeserializer = new DotNetXmlDeserializer();
                ABRPayloadSearchResults modelClassObject = dotNetXmlDeserializer.Deserialize<ABRPayloadSearchResults>(response);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (modelClassObject != null)
                        yield return new ExternalSearchQueryResult<ABRPayloadSearchResults>(query, modelClassObject);
                }
                else if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
                    yield break;
                else if (response.ErrorException != null)
                    throw new AggregateException(response.ErrorException.Message, response.ErrorException);
                else
                    throw new ApplicationException("Could not execute external search query - StatusCode:" + response.StatusCode + "; Content: " + response.Content);
            }        
            
        }

        /// <summary>Builds the clues.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The clues.</returns>
        public override IEnumerable<Clue> BuildClues(ExecutionContext context, IExternalSearchQuery query, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {

           if (result is IExternalSearchQueryResult<ABRPayloadSearchResults> companyResult)
            {

                var code = this.GetOrganizationOriginEntityCode(companyResult, request);

                var clue = new Clue(code, context.Organization);

                this.PopulateCompanyMetadata(clue.Data.EntityData, companyResult, request);
                // TODO: If necessary, you can create multiple clues and return them.

                return new[] { clue };
            }

            return null;


        }

        /// <summary>Gets the primary entity metadata.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The primary entity metadata.</returns>
        public override IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            if (result is IExternalSearchQueryResult<ABRPayloadSearchResults> companyResult)
            {
                if (companyResult != null)
                {
                    return this.CreateCompanyMetadata(companyResult, request);
                }
            }
            return null;
        }

        /// <summary>Gets the preview image.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The preview image.</returns>
        public override IPreviewImage GetPrimaryEntityPreviewImage(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            return null;
        }

        private IEntityMetadata CreateCompanyMetadata(IExternalSearchQueryResult<ABRPayloadSearchResults> resultItem, IExternalSearchRequest request)
        {
            var metadata = new EntityMetadataPart();

            this.PopulateCompanyMetadata(metadata, resultItem, request);

            return metadata;
        }
      
        private EntityCode GetOrganizationOriginEntityCode(IExternalSearchQueryResult<ABRPayloadSearchResults> resultItem, IExternalSearchRequest request)
        {

            return new EntityCode(request.EntityMetaData.EntityType, this.GetCodeOrigin(), request.EntityMetaData.OriginEntityCode.Value);
        }

        /// <summary>Gets the code origin.</summary>
        /// <returns>The code origin</returns>
        private CodeOrigin GetCodeOrigin()
        {
            return CodeOrigin.CluedIn.CreateSpecific("australianBusinessRegistry");
        }
       

        private void PopulateCompanyMetadata(IEntityMetadata metadata, IExternalSearchQueryResult<ABRPayloadSearchResults> resultItem, IExternalSearchRequest request)
        {
            var code = this.GetOrganizationOriginEntityCode(resultItem, request);

            metadata.EntityType = request.EntityMetaData.EntityType;
            metadata.Name = request.EntityMetaData.Name;
            metadata.OriginEntityCode = code;
            metadata.Codes.Add(code);
            metadata.Codes.Add(request.EntityMetaData.OriginEntityCode);

            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.AddressEffectiveFrom] = resultItem.Data.Response.BusinessEntity202001.MainBusinessPhysicalAddress.First().EffectiveFrom.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.AddressEffectiveTo] = resultItem.Data.Response.BusinessEntity202001.MainBusinessPhysicalAddress.First().EffectiveTo.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.AddressPostCode] = resultItem.Data.Response.BusinessEntity202001.MainBusinessPhysicalAddress.First().Postcode.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.AddressStateCode] = resultItem.Data.Response.BusinessEntity202001.MainBusinessPhysicalAddress.First().StateCode.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.BusinessName] = resultItem.Data.Response.BusinessEntity202001.BusinessName.OrganisationName.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.ASICNumber] = resultItem.Data.Response.BusinessEntity202001.ASICNumber.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.BusinessNameEffectiveFrom] = resultItem.Data.Response.BusinessEntity202001.BusinessName.EffectiveFrom.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.EntityDescription] = resultItem.Data.Response.BusinessEntity202001.EntityType.EntityDescription.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.EntityStatusCode] = resultItem.Data.Response.BusinessEntity202001.EntityStatus.EntityStatusCode.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.EntityStatusEffectiveFrom] = resultItem.Data.Response.BusinessEntity202001.EntityStatus.EffectiveFrom.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.EntityStatusEffectiveTo] = resultItem.Data.Response.BusinessEntity202001.EntityStatus.EffectiveTo.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.EntityTypeCode] = resultItem.Data.Response.BusinessEntity202001.EntityType.EntityTypeCode.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.GoodsAndServicesTaxEffectiveFrom] = resultItem.Data.Response.BusinessEntity202001.GoodsAndServicesTax.EffectiveFrom.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.GoodsAndServicesTaxEffectiveTo] = resultItem.Data.Response.BusinessEntity202001.GoodsAndServicesTax.EffectiveTo.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.IsCurrentIdicator] = resultItem.Data.Response.BusinessEntity202001.ABN.IsCurrentIndicator.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.MainName] = resultItem.Data.Response.BusinessEntity202001.MainName.OrganisationName.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.MainNameEffectiveFrom] = resultItem.Data.Response.BusinessEntity202001.MainName.EffectiveFrom.PrintIfAvailable();
            metadata.Properties[AustralianBusinessRegistryVocabulary.Organization.RecordLastUpdated] = resultItem.Data.Response.BusinessEntity202001.RecordLastUpdatedDate.PrintIfAvailable();

        }

        public IEnumerable<EntityType> Accepts(IDictionary<string, object> config, IProvider provider)
        {
            var customTypes = config[Constants.KeyName.AcceptedEntityType].ToString();
            if (string.IsNullOrWhiteSpace(customTypes))
            {
                AcceptedEntityTypes = new EntityType[] { config[Constants.KeyName.AcceptedEntityType].ToString() };
            };

            return AcceptedEntityTypes;
        }

        public IEnumerable<IExternalSearchQuery> BuildQueries(ExecutionContext context, IExternalSearchRequest request, IDictionary<string, object> config, IProvider provider)
        {
            return InternalBuildQueries(context, request, config);
        }

        public IEnumerable<IExternalSearchQueryResult> ExecuteSearch(ExecutionContext context, IExternalSearchQuery query, IDictionary<string, object> config, IProvider provider)
        {
            var jobData = new AustralianBusinessRegistryExternalSearchJobData(config);

            foreach (var externalSearchQueryResult in InternalExecuteSearch(query, jobData.ApiToken)) yield return externalSearchQueryResult;
        }

        public IEnumerable<Clue> BuildClues(ExecutionContext context, IExternalSearchQuery query, IExternalSearchQueryResult result, IExternalSearchRequest request, IDictionary<string, object> config, IProvider provider)
        {
            return BuildClues(context, query, result, request);
        }

        public IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request, IDictionary<string, object> config, IProvider provider)
        {
            return GetPrimaryEntityMetadata(context, result, request);
        }

        public IPreviewImage GetPrimaryEntityPreviewImage(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request, IDictionary<string, object> config, IProvider provider)
        {
            return GetPrimaryEntityPreviewImage(context, result, request);
        }

        public string Icon { get; } = Constants.Icon;
        public string Domain { get; } = Constants.Domain;
        public string About { get; } = Constants.About;

        public AuthMethods AuthMethods { get; } = Constants.AuthMethods;
        public IEnumerable<Control> Properties { get; } = Constants.Properties;
        public Guide Guide { get; } = Constants.Guide;
        public IntegrationType Type { get; } = Constants.IntegrationType;


    }
}