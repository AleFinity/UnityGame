using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject button_e;

    private string name_collider;

    void OnTriggerEnter(Collider other) {
        button_e.SetActive(true);
    }

    //Взаимодействие с нажатием кнопки "E"
    void OnTriggerStay(Collider other)
    {        
        if (Input.GetKeyDown(KeyCode.E)) { 
            if (other.gameObject.tag != "Object") {            
                other.gameObject.SendMessage("Activate");                
                button_e.SetActive(false);
            }
            else
            {
                name_collider = other.gameObject.name;
                other.gameObject.GetComponent<Set_state_object>().active = true;
                Manager.Inventory.AddItem(name_collider);            
                Destroy(other.gameObject);
            }
            button_e.SetActive(false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        button_e.SetActive(false);
    }

    //Взято из книги
    //Физическая сила
    public float pushForce = 10.0f;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {        
        Rigidbody body = hit.collider.attachedRigidbody;    
        if (body != null && !body.isKinematic && hit.gameObject.tag != "Object") 
        {
            hit.gameObject.SendMessage("Activate");
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
