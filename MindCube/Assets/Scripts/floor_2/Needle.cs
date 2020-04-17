using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{   
    void Start()
    {
        if (GetComponent<Set_state_object>().active)
        {
            gameObject.SetActive(false);
        }          
    }
}
