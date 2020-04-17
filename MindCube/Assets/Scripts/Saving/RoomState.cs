//Взято из интернета
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;

public class RoomState
{
	public List<PositData> furniture = new List<PositData>(); // список из перемещаемых предметов

	public RoomState() { }   // пустой конструктор

	public void AddItem(PositData item)
	{   
		furniture.Add(item);            // при генерации дефолтной версии локации
	}

	public void Update()
	{    // функция, по которой данные этого класса-дубликата объектов 
		foreach (PositData felt in furniture) // будут обновляться
			felt.Update();
	}
}

[XmlType("PositionData")]
public class PositData
{
	protected GameObject _inst; // тут храним ссылку на отражаемый объект
	public GameObject inst { set { _inst = value; } }

	[XmlElement("Type")]
	public string Name { get; set; }  // это будет название префаба из Resourses

	[XmlElement("Position")]
	public Vector3 position { get; set; }

	public PositData() { }

	public PositData(string name, Vector3 position)
	{
		this.Name = name;
		this.position = position;
	}

	public virtual void Estate() { }   // для "доработки" объекта после создания

	public virtual void Update()
	{  // обновление нашего рефлектора		
		position = _inst.transform.position;  // согласно реальной информации об объекте
	}
}

[XmlType("Interaction")]
public class Objects_interaction : PositData   // Игрок мог провзаимодействовать с объектом
{
	[XmlAttribute("Interaction")]
	public bool ObjectIsInteraction { get; set; }

	public Objects_interaction() { }

	public Objects_interaction(string name, Vector3 position, bool ObjectIsInteraction) : base(name, position)
	{
		this.ObjectIsInteraction = ObjectIsInteraction;
	}

	public override void Estate()
	{
		_inst.transform.position = position;
		if (!ObjectIsInteraction)			
			_inst.GetComponent<Set_state_object>().active = false;
		else
			_inst.GetComponent<Set_state_object>().active = true;
	}        

	public override void Update()
	{
		if (_inst != null)
		{
			base.Update();
			ObjectIsInteraction = _inst.GetComponent<Set_state_object>().active;
		}
        else { 
			ObjectIsInteraction = true;			
		}
	}
}