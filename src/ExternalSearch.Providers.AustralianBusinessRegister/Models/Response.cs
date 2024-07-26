using System;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models;

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