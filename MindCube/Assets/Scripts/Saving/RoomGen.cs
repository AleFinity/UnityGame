//Взято из интернета
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoomGen : MonoBehaviour
{
	private RoomState state;    // отражающий класс
	private string datapath;    // путь к файлу сохранения для этой локации

	public GameObject[] FurnitureInRoom;

	void Awake()
	{
		datapath = Application.dataPath + "/Saves/SavedData" + Application.loadedLevel + ".xml";
		
		if (File.Exists(datapath))  // если файл сохранения уже существует
		{
			state = Serializator.DeXml(datapath);  // считываем state оттуда
		}
		else
		{
			setDefault();       // иначе задаём дефолт
		}
		Generate(); // 	генерируем локацию по информации из state
	}

	void setDefault()
	{
		state = new RoomState();		
		foreach (GameObject furniture in FurnitureInRoom)
		{
			state.AddItem(new Objects_interaction(furniture.gameObject.name, furniture.transform.position, false)); // взаимодействия с объектом не было
		}
	}

	void Generate()
	{
		foreach (PositData felt in state.furniture)
		{  // для всех предметов в комнате
			if (GameObject.Find(felt.Name) != null) {
				felt.inst = GameObject.Find(felt.Name);
				// овеществляем их
				felt.Estate(); // и задаём дополнительные параметры
			}
		}
	}

	void Dump()
	{
		state.Update(); // вызов обновления state
		Serializator.SaveXml(state, datapath); // и его сериализация
	}
}
