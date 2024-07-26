using System;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models;

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