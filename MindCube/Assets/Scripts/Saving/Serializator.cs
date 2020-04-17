//Взято из интернета
using System.Xml.Serialization;
using System;
using System.IO;

public class Serializator
{
	static public void SaveXml(RoomState state, string datapath)
	{
		Type[] extraTypes = { typeof(PositData), typeof(Objects_interaction) };
		XmlSerializer serializer = new XmlSerializer(typeof(RoomState), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Create);
		serializer.Serialize(fs, state);
		fs.Close();
	}

	static public RoomState DeXml(string datapath)
	{
		Type[] extraTypes = { typeof(PositData), typeof(Objects_interaction) };
		XmlSerializer serializer = new XmlSerializer(typeof(RoomState), extraTypes);

		FileStream fs = new FileStream(datapath, FileMode.Open);
		RoomState state = (RoomState)serializer.Deserialize(fs);
		fs.Close();

		return state;
	}
}
