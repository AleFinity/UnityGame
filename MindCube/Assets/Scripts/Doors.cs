using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    public Camera cam;
    public GameObject targ;
    public GameObject save_state;

    public string this_floor;
    public string floor;//название новой сцены
    public string side;
    public bool lock_flag; //замок на двери 
    public int number;

    private bool near = false;
    private Vector3 cam_pos;
    private Vector3 cam_rot;
    
    public GameObject button_e;

    public bool get_near() { return near; }

    void Activate()
    {        
        if (!lock_flag)
        {
            PlayerPrefs.SetInt(this_floor, number);
            save_state.SendMessageUpwards("Dump");
            GameObject.Find("Heroe").SendMessageUpwards("DumpInv");
            SceneManager.LoadScene(floor, LoadSceneMode.Single);
            return;
        }
        else {
            if (near == false) {
                near = true;
                cam_pos =cam.transform.position;
                cam_rot = cam.transform.localEulerAngles;
                cam.transform.localEulerAngles = new Vector3(0, -180, 0);
                cam.transform.position = targ.transform.position;
            }            
        }
    }
    void Update() {
        if (near) {
            if (Input.GetAxis("Horizontal") != 0) {
                near = false;
                GameObject.Find("Canvas(Clone)").gameObject.transform.GetChild(1).gameObject.SetActive(true); // ???
                set_cam();
            }
        }
    }
    public void set_cam() {
        cam.transform.position = cam_pos;
        cam.transform.localEulerAngles = cam_rot;
    }
}
