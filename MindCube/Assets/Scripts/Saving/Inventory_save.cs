using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Inventory_save : MonoBehaviour
{
	private Inventory state;    // отражающий класс
	private string datapath;    // путь к файлу сохранения для этой локации
	
	void Awake()
	{
		datapath = Application.dataPath + "/Saves/Inventory.xml";

		if (File.Exists(datapath))  // если файл сохранения уже существует
		{
			state = SerializatorInv.DeXml(datapath);  // считываем state оттуда
		}
		else
		{
			setDefault();       // иначе задаём дефолт
		}
	}

	void setDefault()
	{
		state = new Inventory();		
	}

	public void Generate()
	{
		foreach (string inv in state.inventories)
		{  // для всех предметов в комнате
			Manager.Inventory.AddItem(inv);
		}
	}

	void DumpInv()
	{
		File.Delete(datapath);
		state.Update(); // вызов обновления state
		SerializatorInv.SaveXml(state, datapath); // и его сериализация
	}
}
[XmlType("Name")]
public class Inventory
{
	public List<string> inventories = new List<string>(); // список из перемещаемых предметов

	public Inventory() { }   // пустой конструктор
	
	public void AddItem(string name)
	{
		inventories.Add(name);
	}

	public void Update()
	{
		inventories= Manager.Inventory.GetItemList();
	}
}

public class SerializatorInv
{
	static public void SaveXml(Inventory state, string datapath)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(Inventory));

		FileStream fs = new FileStream(datapath, FileMode.Create);
		serializer.Serialize(fs, state);
		fs.Close();
	}

	static public Inventory DeXml(string datapath)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(Inventory));

		FileStream fs = new FileStream(datapath, FileMode.Open);
		Inventory state = (Inventory)serializer.Deserialize(fs);
		fs.Close();

		return state;
	}
}