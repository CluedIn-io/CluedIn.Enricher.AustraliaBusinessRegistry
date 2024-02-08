using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegistry.Vocabularies
{
    public class AustralianBusinessRegistryDetailsVocabulary : SimpleVocabulary
    {
        public AustralianBusinessRegistryDetailsVocabulary()
        {
            this.VocabularyName = "Australian Business Registry Details";
            this.KeyPrefix = "australianBusinessRegistry.Company";
            this.KeySeparator = ".";
            this.Grouping = EntityType.Organization;

            this.AddGroup("AustralianBusinessRegistry Image Details", group =>
            {
                this.MainName = group.Add(new VocabularyKey("mainName", VocabularyKeyDataType.Text));
                this.RecordLastUpdated = group.Add(new VocabularyKey("recordLastUpdated", VocabularyKeyDataType.Text));
                this.IsCurrentIdicator = group.Add(new VocabularyKey("isCurrentIdicator", VocabularyKeyDataType.Text));
                this.EntityStatusCode = group.Add(new VocabularyKey("entityStatusCode", VocabularyKeyDataType.Text));
                this.EntityStatusEffectiveFrom = group.Add(new VocabularyKey("entityStatusEffectiveFrom", VocabularyKeyDataType.Text));
                this.EntityStatusEffectiveTo = group.Add(new VocabularyKey("entityStatusEffectiveTo", VocabularyKeyDataType.Text));
                this.ASICNumber = group.Add(new VocabularyKey("asicNumber", VocabularyKeyDataType.Text));
                this.EntityTypeCode = group.Add(new VocabularyKey("entityTypeCode", VocabularyKeyDataType.Text));
                this.EntityDescription = group.Add(new VocabularyKey("entityDescription", VocabularyKeyDataType.Text));
                this.GoodsAndServicesTaxEffectiveFrom = group.Add(new VocabularyKey("goodsAndServicesTaxEffectiveFrom", VocabularyKeyDataType.Text));
                this.GoodsAndServicesTaxEffectiveTo = group.Add(new VocabularyKey("goodsAndServicesTaxEffectiveTo", VocabularyKeyDataType.Text));
                this.MainNameEffectiveFrom = group.Add(new VocabularyKey("mainNameEffectiveFrom", VocabularyKeyDataType.Text));
                this.AddressStateCode = group.Add(new VocabularyKey("addressStateCode", VocabularyKeyDataType.Text));
                this.AddressPostCode = group.Add(new VocabularyKey("addressPostCode", VocabularyKeyDataType.Text));
                this.AddressEffectiveFrom = group.Add(new VocabularyKey("addressEffectiveFrom", VocabularyKeyDataType.Text));
                this.AddressEffectiveTo = group.Add(new VocabularyKey("addressEffectiveTo", VocabularyKeyDataType.Text));
                this.BusinessName = group.Add(new VocabularyKey("businessName", VocabularyKeyDataType.Text));
                this.BusinessNameEffectiveFrom = group.Add(new VocabularyKey("businessNameEffectiveFrom", VocabularyKeyDataType.Text));
            });
        }

        public VocabularyKey MainName { get; set; }
        public VocabularyKey RecordLastUpdated { get; set; }
        public VocabularyKey IsCurrentIdicator { get; set; }
        public VocabularyKey EntityStatusCode { get; set; }
        public VocabularyKey EntityStatusEffectiveFrom { get; set; }
        public VocabularyKey EntityStatusEffectiveTo { get; set; }
        public VocabularyKey ASICNumber { get; set; }
        public VocabularyKey EntityTypeCode { get; set; }
        public VocabularyKey EntityDescription { get; set; }
        public VocabularyKey GoodsAndServicesTaxEffectiveFrom { get; set; }
        public VocabularyKey GoodsAndServicesTaxEffectiveTo { get; set; }
        public VocabularyKey MainNameEffectiveFrom { get; set; }
        public VocabularyKey AddressStateCode { get; set; }
        public VocabularyKey AddressPostCode { get; set; }
        public VocabularyKey AddressEffectiveFrom { get; set; }
        public VocabularyKey AddressEffectiveTo { get; set; }
        public VocabularyKey BusinessName { get; set; }
        public VocabularyKey BusinessNameEffectiveFrom { get; set; }

    }

}