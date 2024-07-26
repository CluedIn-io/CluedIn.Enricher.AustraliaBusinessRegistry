using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models;

[XmlRoot(ElementName = "request")]
public class Request
{

    [XmlElement(ElementName = "identifierSearchRequest")]
    public IdentifierSearchRequest IdentifierSearchRequest { get; set; }
}