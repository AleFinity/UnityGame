using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chest_open;
    public GameObject key_attic;

    private int interact;

    void Start()
    {
        if (GetComponent<Set_state_object>().active)
        {
            if (key_attic.GetComponent<Set_state_object>().active)
            {
                Destroy(key_attic.gameObject);
            }
            else
                key_attic.gameObject.SetActive(true);

            chest_open.gameObject.transform.position = transform.position;
            chest_open.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
        else
            key_attic.gameObject.SetActive(false);
    }

    void Activate()
    {
        if (Manager.Inventory.GetItemList().IndexOf("key") != -1)
        {
            Manager.Inventory.DelItem("key");
            key_attic.SetActive(true);
            chest_open.gameObject.SetActive(true);
            chest_open.gameObject.transform.position = transform.position;
            key_attic.gameObject.GetComponent<Rigidbody>().AddForce(0, 0.5f, 0.5f); ;            
            Destroy(this.gameObject);
            GetComponent<Set_state_object>().active = true;
        }
    }
}