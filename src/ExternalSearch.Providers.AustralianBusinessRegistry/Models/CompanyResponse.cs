using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegistry.Models
{

    [XmlRoot(ElementName = "identifierSearchRequest")]
    public class IdentifierSearchRequest
    {

        [XmlElement(ElementName = "authenticationGUID")]
        public string AuthenticationGUID { get; set; }

        [XmlElement(ElementName = "identifierType")]
        public string IdentifierType { get; set; }

        [XmlElement(ElementName = "identifierValue")]
        public double IdentifierValue { get; set; }

        [XmlElement(ElementName = "history")]
        public string History { get; set; }
    }

    [XmlRoot(ElementName = "request")]
    public class Request
    {

        [XmlElement(ElementName = "identifierSearchRequest")]
        public IdentifierSearchRequest IdentifierSearchRequest { get; set; }
    }

    [XmlRoot(ElementName = "ABN")]
    public class ABN
    {

        [XmlElement(ElementName = "identifierValue")]
        public double IdentifierValue { get; set; }

        [XmlElement(ElementName = "isCurrentIndicator")]
        public string IsCurrentIndicator { get; set; }

        [XmlElement(ElementName = "replacedFrom")]
        public DateTime ReplacedFrom { get; set; }
    }

    [XmlRoot(ElementName = "entityStatus")]
    public class EntityStatus
    {

        [XmlElement(ElementName = "entityStatusCode")]
        public string EntityStatusCode { get; set; }

        [XmlElement(ElementName = "effectiveFrom")]
        public DateTime EffectiveFrom { get; set; }

        [XmlElement(ElementName = "effectiveTo")]
        public DateTime EffectiveTo { get; set; }
    }

    [XmlRoot(ElementName = "entityType")]
    public class EntityType
    {

        [XmlElement(ElementName = "entityTypeCode")]
        public string EntityTypeCode { get; set; }

        [XmlElement(ElementName = "entityDescription")]
        public string EntityDescription { get; set; }
    }

    [XmlRoot(ElementName = "goodsAndServicesTax")]
    public class GoodsAndServicesTax
    {

        [XmlElement(ElementName = "effectiveFrom")]
        public DateTime EffectiveFrom { get; set; }

        [XmlElement(ElementName = "effectiveTo")]
        public DateTime EffectiveTo { get; set; }
    }

    [XmlRoot(ElementName = "mainName")]
    public class MainName
    {

        [XmlElement(ElementName = "organisationName")]
        public string OrganisationName { get; set; }

        [XmlElement(ElementName = "effectiveFrom")]
        public DateTime EffectiveFrom { get; set; }
    }

    [XmlRoot(ElementName = "mainBusinessPhysicalAddress")]
    public class MainBusinessPhysicalAddress
    {

        [XmlElement(ElementName = "stateCode")]
        public string StateCode { get; set; }

        [XmlElement(ElementName = "postcode")]
        public int Postcode { get; set; }

        [XmlElement(ElementName = "effectiveFrom")]
        public DateTime EffectiveFrom { get; set; }

        [XmlElement(ElementName = "effectiveTo")]
        public DateTime EffectiveTo { get; set; }
    }

    [XmlRoot(ElementName = "businessName")]
    public class BusinessName
    {

        [XmlElement(ElementName = "organisationName")]
        public string OrganisationName { get; set; }

        [XmlElement(ElementName = "effectiveFrom")]
        public DateTime EffectiveFrom { get; set; }
    }

    [XmlRoot(ElementName = "businessEntity202001")]
    public class BusinessEntity202001
    {

        [XmlElement(ElementName = "recordLastUpdatedDate")]
        public DateTime RecordLastUpdatedDate { get; set; }

        [XmlElement(ElementName = "ABN")]
        public ABN ABN { get; set; }

        [XmlElement(ElementName = "entityStatus")]
        public EntityStatus EntityStatus { get; set; }

        [XmlElement(ElementName = "ASICNumber")]
        public int ASICNumber { get; set; }

        [XmlElement(ElementName = "entityType")]
        public EntityType EntityType { get; set; }

        [XmlElement(ElementName = "goodsAndServicesTax")]
        public GoodsAndServicesTax GoodsAndServicesTax { get; set; }

        [XmlElement(ElementName = "mainName")]
        public MainName MainName { get; set; }

        [XmlElement(ElementName = "mainBusinessPhysicalAddress")]
        public List<MainBusinessPhysicalAddress> MainBusinessPhysicalAddress { get; set; }

        [XmlElement(ElementName = "businessName")]
        public BusinessName BusinessName { get; set; }
    }

    [XmlRoot(ElementName = "response")]
    public class Response
    {

        [XmlElement(ElementName = "usageStatement")]
        public string UsageStatement { get; set; }

        [XmlElement(ElementName = "dateRegisterLastUpdated")]
        public DateTime DateRegisterLastUpdated { get; set; }

        [XmlElement(ElementName = "dateTimeRetrieved")]
        public DateTime DateTimeRetrieved { get; set; }

        [XmlElement(ElementName = "businessEntity202001")]
        public BusinessEntity202001 BusinessEntity202001 { get; set; }
    }

    [XmlRoot(Namespace = "http://abr.business.gov.au/ABRXMLSearch/", ElementName = "ABRPayloadSearchResults", DataType = "string", IsNullable = true)]
    public class ABRPayloadSearchResults
    {

        [XmlElement(ElementName = "request")]
        public Request Request { get; set; }

        [XmlElement(ElementName = "response")]
        public Response Response { get; set; }

        [XmlAttribute(AttributeName = "xsd")]
        public string Xsd { get; set; }

        [XmlAttribute(AttributeName = "xsi")]
        public string Xsi { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }



}