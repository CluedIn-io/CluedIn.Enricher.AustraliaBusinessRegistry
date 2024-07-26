using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models;

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