using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_key : MonoBehaviour
{
    private bool open = false;
    private Vector3 target = new Vector3(2.065f, 1.06f, 1.681f);

    void Start()
    {
        if (GetComponent<Set_state_object>().active)
        {
            transform.localEulerAngles = new Vector3(-270, 180, 281);
            transform.position = new Vector3(2.101335f, 1.022f, 1.291805f);
        }
    }

    void Activate()
    {        
        if (Manager.Inventory.GetItemList().IndexOf("key_attic") != -1)
        {
            Manager.Inventory.DelItem("key_attic");
            open = true;
            GetComponent<Set_state_object>().active = true;
        }
    }
    void Update ()
    {        
        if (transform.rotation.z<-0.55 && open == true)
            transform.RotateAround(target, Vector3.up, 100 * Time.deltaTime);
        else
            open = false;
    }
}
