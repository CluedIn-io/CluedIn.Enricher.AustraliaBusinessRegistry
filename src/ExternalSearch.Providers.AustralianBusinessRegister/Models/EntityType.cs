using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models;

[XmlRoot(ElementName = "entityType")]
public class EntityType
{

    [XmlElement(ElementName = "entityTypeCode")]
    public string EntityTypeCode { get; set; }

    [XmlElement(ElementName = "entityDescription")]
    public string EntityDescription { get; set; }
}