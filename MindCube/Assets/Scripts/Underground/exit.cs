using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour
{
    public bool see_exit=false; //если свет был выключен, то игрок увидел выход
    public GameObject save_state;
    
    void Activate()
    {
        if (see_exit) {
            save_state.SendMessageUpwards("Dump");
            SceneManager.LoadScene("first_floor", LoadSceneMode.Single);
        }
    }
}
