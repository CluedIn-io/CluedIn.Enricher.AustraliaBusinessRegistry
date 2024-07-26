using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.AustralianBusinessRegister.Models;

[XmlRoot(ElementName = "businessEntity202001")]
public class BusinessEntity202001
{

    [XmlElement(ElementName = "recordLastUpdatedDate")]
    public DateTime RecordLastUpdatedDate { get; set; }

    [XmlElement(ElementName = "ABN")]
    public ABN ABN { get; set; }

    [XmlElement(ElementName = "entityStatus")]
    public EntityStatus EntityStatus { get; set; }

    [XmlElement(ElementName = "ASICNumber")]
    public int ASICNumber { get; set; }

    [XmlElement(ElementName = "entityType")]
    public EntityType EntityType { get; set; }

    [XmlElement(ElementName = "goodsAndServicesTax")]
    public GoodsAndServicesTax GoodsAndServicesTax { get; set; }

    [XmlElement(ElementName = "mainName")]
    public MainName MainName { get; set; }

    [XmlElement(ElementName = "mainBusinessPhysicalAddress")]
    public List<MainBusinessPhysicalAddress> MainBusinessPhysicalAddress { get; set; }

    [XmlElement(ElementName = "businessName")]
    public BusinessName BusinessName { get; set; }
}