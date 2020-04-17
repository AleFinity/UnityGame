using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public string where_move; // left right up down

    private bool moved = false;
    private int turn_on = -1; // -1-выкл  1-вкл    
    private Vector3 new_pos;

    void Start()
    {
        if (GetComponent<Set_state_object>().active) 
            turn_on = 1;
    }

    public bool Get_moved() {
        return moved;
    }

    void Move()
    {
        if (moved == false) { 
            turn_on *= -1;        
            moved = true;
            if (where_move == "left")
            {
                new_pos = transform.position;
                new_pos.x -= turn_on * (-1);
            }
            else if (where_move == "right")
            {
                new_pos = transform.position;
                new_pos.x += turn_on * (-1);
            }
            else if (where_move == "up")
            {
                new_pos = transform.position;
                new_pos.x -= turn_on * (-1);
            }
            else if (where_move == "down")
            {
                new_pos = transform.position;
                new_pos.y += turn_on * (-1);
            }
            if (turn_on==1)
                GetComponent<Set_state_object>().active = true;
            else
                GetComponent<Set_state_object>().active = false;
        }
    }

    void Update() {
        if (moved == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new_pos, 1 * Time.deltaTime);
            if (transform.position == new_pos)
                moved = false;
        }
    }
}
