using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_window : MonoBehaviour
{
    public GameObject target;
    public int side;//1-right 2-left

    private bool open = false;

    void Open_w() {
        open = true;
    }

    void Update()
    {
        if(open)
            if (transform.rotation.y<0.9547998f && side==1)
                transform.RotateAround(target.transform.position, Vector3.up, 100 * Time.deltaTime);
            else if (transform.rotation.y > -0.9547998f && side == 2)
                transform.RotateAround(target.transform.position, Vector3.down, 100 * Time.deltaTime);
            else
                open = false;        
    }
}
