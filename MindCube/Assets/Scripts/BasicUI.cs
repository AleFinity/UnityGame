//Взято из книги
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    void OnGUI()
    {
        int posX = 10; 
        int posY = 10; 
        int width = 100; 
        int height = 100; 
        int buffer = 10;

        List<string> itemList = Manager.Inventory.GetItemList(); 
        
        foreach (string item in itemList)
        {
            int count = Manager.Inventory.GetItemCount(item);
            Texture2D image = Resources.Load<Texture2D>("Items/" + item);
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent(image)); 
            posX += width + buffer;
        }
    }
} 