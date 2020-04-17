using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_arm : MonoBehaviour
{
    public GameObject[] platforms;

    void Start() {
        if (GetComponent<Set_state_object>().active)
            transform.Rotate(0, 0, 180);
    }

    void Activate()
    {
        Platform other;
        other = platforms[0].gameObject.GetComponent("Platform") as Platform;        
        if (other.Get_moved() == false) { 
            foreach(GameObject platform in platforms)
                platform.gameObject.SendMessage("Move");
            transform.Rotate(0, 0, 180);
            GetComponent<Set_state_object>().active = true;
        }
    }
}
