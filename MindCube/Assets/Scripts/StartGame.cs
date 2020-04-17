using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject Heroe_pr;
    public GameObject Canvas_pr;
    public GameObject camera;

    private GameObject heroe;
    private GameObject canv;

    private int restart;

    void Awake()
    {
        heroe = GameObject.Find("Heroe");
        canv = GameObject.Find("Canvas(Clone)");
        if (heroe == null)
        {
            if (canv == null)
            {
                Instantiate(Canvas_pr, new Vector3(0, 0, 0), Quaternion.identity);
                canv = GameObject.Find("Canvas(Clone)");
                canv.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
                Canvas_pr.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            Instantiate(Heroe_pr, new Vector3(-1, 0.45f, 2.5f), Quaternion.identity);            

            heroe = GameObject.Find("Heroe(Clone)");            
            heroe.gameObject.name = "Heroe"; 

            heroe.gameObject.GetComponent<Movings>().camera = camera;
            heroe.gameObject.GetComponent<Interaction>().button_e = canv.gameObject.transform.GetChild(1).gameObject;
        }        
        else
        {
            Canvas_pr.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            canv = GameObject.Find("Canvas(Clone)");
            heroe.transform.position = new Vector3(-1, 0.45f, 2.5f);
            heroe.gameObject.GetComponent<Movings>().camera = camera;
            heroe.gameObject.GetComponent<Interaction>().button_e = canv.gameObject.transform.GetChild(1).gameObject;
        }
    }
}