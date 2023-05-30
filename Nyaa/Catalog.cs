// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Rss));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Rss)serializer.Deserialize(reader);
// }

[XmlRoot(ElementName="link")]
public class Link { 

	[XmlAttribute(AttributeName="href")] 
	public string Href { get; set; } 

	[XmlAttribute(AttributeName="rel")] 
	public string Rel { get; set; } 

	[XmlAttribute(AttributeName="type")] 
	public string Type { get; set; } 
}

[XmlRoot(ElementName="guid")]
public class Guid { 

	[XmlAttribute(AttributeName="isPermaLink")] 
	public bool IsPermaLink { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="item")]
public class Item { 

	[XmlElement(ElementName="title")] 
	public string Title { get; set; } 

	[XmlElement(ElementName="link")] 
	public string Link { get; set; } 

	[XmlElement(ElementName="guid")] 
	public Guid Guid { get; set; } 

	[XmlElement(ElementName="pubDate")] 
	public DateTime PubDate { get; set; } 

	[XmlElement(ElementName="seeders")] 
	public int Seeders { get; set; } 

	[XmlElement(ElementName="leechers")] 
	public int Leechers { get; set; } 

	[XmlElement(ElementName="downloads")] 
	public int Downloads { get; set; } 

	[XmlElement(ElementName="infoHash")] 
	public string InfoHash { get; set; } 

	[XmlElement(ElementName="categoryId")] 
	public string CategoryId { get; set; } 

	[XmlElement(ElementName="category")] 
	public string Category { get; set; } 

	[XmlElement(ElementName="size")] 
	public string Size { get; set; } 

	[XmlElement(ElementName="comments")] 
	public int Comments { get; set; } 

	[XmlElement(ElementName="trusted")] 
	public string Trusted { get; set; } 

	[XmlElement(ElementName="remake")] 
	public string Remake { get; set; } 

	[XmlElement(ElementName="description")] 
	public string Description { get; set; } 
}

[XmlRoot(ElementName="channel")]
public class Channel { 

	[XmlElement(ElementName="title")] 
	public string Title { get; set; } 

	[XmlElement(ElementName="description")] 
	public string Description { get; set; } 

	[XmlElement(ElementName="link")] 
	public List<string> Link { get; set; } 

	[XmlElement(ElementName="item")] 
	public List<Item> Item { get; set; } 
}

[XmlRoot(ElementName="rss")]
public class Rss { 

	[XmlElement(ElementName="channel")] 
	public Channel Channel { get; set; } 

	[XmlAttribute(AttributeName="atom")] 
	public string Atom { get; set; } 

	[XmlAttribute(AttributeName="nyaa")] 
	public string Nyaa { get; set; } 

	[XmlAttribute(AttributeName="version")] 
	public double Version { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

