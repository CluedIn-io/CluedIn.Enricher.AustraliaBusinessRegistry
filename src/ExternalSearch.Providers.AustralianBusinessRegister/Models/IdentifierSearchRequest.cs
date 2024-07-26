using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models
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
}