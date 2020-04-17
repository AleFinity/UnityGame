using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baloon : MonoBehaviour
{
    private CharacterController _charController;
    public GameObject key;

    private int interact; 

    void Start()
    {
        if (GetComponent<Set_state_object>().active)
        {
            if(key != null) {
                if (!key.gameObject.GetComponent<Set_state_object>().active)
                {
                    key.gameObject.SetActive(true);
                    key.transform.parent = this.transform.parent;
                }
                else
                    Destroy(key.gameObject);
            }
            Destroy(this.gameObject);
        }
        else
            if(key!=null)
                key.gameObject.SetActive(false); ;
    }

    void Activate() {
        if (Manager.Inventory.GetItemList().IndexOf("needle")!=-1)
        {
            StartCoroutine(AudioCoroutine());            
            if (key != null) {
                key.SetActive(true);
                key.transform.parent = this.transform.parent;
                Manager.Inventory.DelItem("needle");
            }
            GetComponent<Set_state_object>().active = true;
        }        
    }

    IEnumerator AudioCoroutine()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().enabled=(false);
        GetComponent<CapsuleCollider>().enabled = (false);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
