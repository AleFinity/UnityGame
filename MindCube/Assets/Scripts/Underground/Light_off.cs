using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_off : MonoBehaviour
{
    public GameObject[] lamps;
    public GameObject[] luke_light;

    [SerializeField] public GameObject camera;

    private bool flag_on = true;

    void Start() {        
        if (GetComponent<Set_state_object>().active) 
            Activate();         
    }

    void Activate()
    {        
        if (flag_on == true) {
            foreach (GameObject lamp in lamps)
            {
                lamp.GetComponent<Light>().enabled = false;
                lamp.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            }
            foreach (GameObject lights in luke_light)
            {
                lights.GetComponent<Light>().enabled = true;
            }
            camera.GetComponent<Camera>().backgroundColor = new Color32(48, 48, 48, 0);
            transform.position = new Vector3(2.5f, 0.5f, 1.94f);
            flag_on = false;

            FindObjectOfType<exit>().see_exit = true; // увидел люк
            GetComponent<Set_state_object>().active = true;
        }
        else
        {
            foreach (GameObject lamp in lamps)
            {
                lamp.GetComponent<Light>().enabled = true;
                lamp.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            }
            foreach (GameObject lights in luke_light)
            {
                lights.GetComponent<Light>().enabled = false;
            }
            camera.GetComponent<Camera>().backgroundColor = new Color32(164, 164, 164, 0);
            transform.position = new Vector3(2.5f, 0.5f, 1.9f);
            flag_on = true;
            FindObjectOfType<exit>().see_exit = true;
            GetComponent<Set_state_object>().active = false;
        }
    }
}          

