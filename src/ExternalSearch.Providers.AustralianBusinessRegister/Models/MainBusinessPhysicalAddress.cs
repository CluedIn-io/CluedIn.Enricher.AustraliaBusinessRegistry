using System;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models;

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