using System;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models;

[XmlRoot(ElementName = "businessName")]
public class BusinessName
{

    [XmlElement(ElementName = "organisationName")]
    public string OrganisationName { get; set; }

    [XmlElement(ElementName = "effectiveFrom")]
    public DateTime EffectiveFrom { get; set; }
}