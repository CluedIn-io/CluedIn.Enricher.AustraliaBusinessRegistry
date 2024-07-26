using System;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models;

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