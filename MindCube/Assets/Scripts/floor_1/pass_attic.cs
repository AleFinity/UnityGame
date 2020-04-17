using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class pass_attic : MonoBehaviour
{
    public GameObject door;
    public GameObject txt;
    public string password;

    private bool check = false;
    private bool open = false;

    void Start()
    {
        if (GetComponent<Set_state_object>().active)
        {
            Doors other;
            other = door.gameObject.GetComponent("Doors") as Doors;
            other.lock_flag = false;
            open = true;
        }
    }

    void Push_button(int number)
    {
        string old_text = txt.gameObject.GetComponent<TextMesh>().text;
        if (!check && !open) {
            check = true;
            if (number == -1)
            {
                txt.gameObject.GetComponent<TextMesh>().text = old_text.Substring(old_text.Length - 1, old_text.Length - 1);
                check = false;
            }
            else
            {
                txt.gameObject.GetComponent<TextMesh>().text = old_text + number.ToString();
                if (txt.gameObject.GetComponent<TextMesh>().text.Length == 4)
                {
                    if (txt.gameObject.GetComponent<TextMesh>().text == password)
                    {       
                        txt.gameObject.GetComponent<TextMesh>().color = new Color(0, 255, 0);
                        open = true;

                        Doors other;
                        other = door.gameObject.GetComponent("Doors") as Doors;
                        other.lock_flag = false;
                        other.set_cam();
                        GetComponent<Set_state_object>().active = true;
                    }
                    else
                        StartCoroutine(TextCoroutine());
                }
                else
                    check = false;
            }
        }
    }

    IEnumerator TextCoroutine()
    {
        for (int i = 0; i < 2; i++)
        {
            txt.gameObject.GetComponent<TextMesh>().color = new Color(255,0,0);
            yield return new WaitForSeconds(0.2f);
            txt.gameObject.GetComponent<TextMesh>().color = new Color(255, 255, 255);
            yield return new WaitForSeconds(0.2f);
        }
        txt.gameObject.GetComponent<TextMesh>().text = "";
        check = false;
    }

}
