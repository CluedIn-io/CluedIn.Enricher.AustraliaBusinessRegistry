using System;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models;

[XmlRoot(ElementName = "goodsAndServicesTax")]
public class GoodsAndServicesTax
{

    [XmlElement(ElementName = "effectiveFrom")]
    public DateTime EffectiveFrom { get; set; }

    [XmlElement(ElementName = "effectiveTo")]
    public DateTime EffectiveTo { get; set; }
}