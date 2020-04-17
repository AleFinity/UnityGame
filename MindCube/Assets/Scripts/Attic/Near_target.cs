using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Near_target : MonoBehaviour
{
    public GameObject heroe;
    public GameObject[] glasses;
    public GameObject particle;

    private bool come_to_wind = false;
    private Vector3 new_pos;
    private Movings heroe_moving_script;

    void Start() {
        heroe = GameObject.Find("Heroe");
        new_pos = transform.position;
        new_pos.z += 0.5f;
        heroe_moving_script = heroe.gameObject.GetComponent("Movings") as Movings;
    }

    void Update() {        
        if(Vector3.Distance(heroe.transform.position, transform.position)<0.3f && come_to_wind == false)
        {
            foreach(GameObject glass in glasses)
                glass.gameObject.SendMessage("Open_w");

            heroe_moving_script.enabled = false;
            heroe.transform.localEulerAngles = new Vector3(0, 0, 0);
            come_to_wind = true;            
        }
        if (come_to_wind) {            
            heroe.transform.position = Vector3.MoveTowards(heroe.transform.position, new_pos, 1 * Time.deltaTime) ;                    
        }
        if (heroe.transform.position == new_pos) {
            heroe_moving_script.set_animator(0);
            particle.SetActive(true);
            heroe.transform.localScale = Vector3.MoveTowards(heroe.transform.localScale, new Vector3(0,0,0), 0.05f * Time.deltaTime);
            StartCoroutine(FinishCoroutine());
        }
    }
    IEnumerator FinishCoroutine()
    {        
        yield return new WaitForSeconds(3f);
        GameObject canv = GameObject.Find("Canvas(Clone)");
        canv.GetComponent<Menu>().Paused();
        canv.GetComponent<Menu>().PauseMenuUI.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        canv.GetComponent<Menu>().PauseMenuUI.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }
}
