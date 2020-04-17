using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroe_on_scenes : MonoBehaviour
{
    public GameObject heroe;
    public GameObject camera;
    public GameObject[] spawn;
    public string scenes;
    private int number_spawn;

    void Awake()
    {
        number_spawn = PlayerPrefs.GetInt(scenes, 0);

        Vector3 pos_spawn = transform.TransformDirection(spawn[number_spawn].transform.GetChild(0).transform.position);

        heroe = GameObject.Find("Heroe");
        Movings other;
        other = heroe.gameObject.GetComponent("Movings") as Movings;
        other.camera = camera;
        heroe.transform.position = pos_spawn;
        if (heroe.gameObject.GetComponent<AudioSource>().enabled == false)
            heroe.gameObject.GetComponent<AudioSource>().enabled = true;

        Doors door_s = spawn[number_spawn].gameObject.GetComponent("Doors") as Doors;
        if (door_s!=null) {
            other.side = door_s.side;
            if (door_s.side == "left") { 
                camera.transform.position = new Vector3(7, 2.85f, 1.5f);
                camera.transform.localEulerAngles = new Vector3(10, -100, 0);
            }
            else if (door_s.side == "right") {
                camera.transform.position = new Vector3(1.5f, 2.85f, 7);
                camera.transform.localEulerAngles = new Vector3(10, -170, 0);
            }
        }
    }
}
