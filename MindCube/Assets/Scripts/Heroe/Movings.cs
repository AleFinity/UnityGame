using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movings : MonoBehaviour
{
    public float speed = 1f;
    private float speed_run=0;

    public string side;
    public float gravity = -9.8f;

    public GameObject camera;
    private CharacterController _charController; 

    private bool flag_ray;
    private bool jump = false;

    private Animator _animator;

    public void set_animator(float speed_anim) {
        _animator.SetFloat("Speed", speed_anim);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (side == "left") 
            camera.transform.position = new Vector3(7, 2.85f, 1.5f);
        else if (side == "right")
            camera.transform.position = new Vector3(1.5f, 2.85f, 7);
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Вектор движения (верх - низ)
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Mathf.Round(transform.localEulerAngles.z) == 0)  
                flag_ray = false; //луч вверх
            else 
                flag_ray = true; //луч вниз

            transform.localEulerAngles += new Vector3(0, 0, 180);
            
            RaycastHit hit;
            Ray ray_platform;

            if (flag_ray == false)//луч вверх
            { 
                ray_platform = new Ray(transform.position, Vector3.up);
                if (Physics.Raycast(ray_platform, out hit)) { 
                    transform.position = new Vector3(transform.position.x, hit.point.y - 0.25f, transform.position.z);
                    gravity *= (-1);
                    jump = true;
                }
            }                
            else//луч вниз
            {
                ray_platform = new Ray(transform.position, Vector3.down);
                if (Physics.Raycast(ray_platform, out hit)) { 
                    transform.position = new Vector3(transform.position.x, hit.point.y + 0.25f, transform.position.z);
                    gravity *= (-1);
                    jump = true;
                }
            }            
        }
        //Вектор движения (право - лево)
        float delta = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector3 movement = new Vector3(0, 0, Mathf.Abs(delta));
        movement = transform.TransformDirection(movement);
        movement.y = gravity * Time.deltaTime; // задаём направление гравитации
            

        if (side == "left") {//Левая сторона куба
            if (delta > 0) //Выбор направления движения
                transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
            else if (delta < 0)
                transform.localEulerAngles = new Vector3(0, -180, transform.localEulerAngles.z);
        }        
        else if (side == "right") {//Правая сторона куба
            if (delta > 0)
                transform.localEulerAngles = new Vector3(0, -90, transform.localEulerAngles.z);
            else if (delta < 0)
                transform.localEulerAngles = new Vector3(0, 90, transform.localEulerAngles.z);           
        }

        if (delta != 0)
        {
            speed_run = speed;
            _charController.Move(movement);// перемещение вдоль стороны
        }
        else
        {
            speed_run = 0;
            Vector3 old_pos = transform.position;                       
            if(jump)
               _charController.Move(new Vector3(0, gravity * Time.deltaTime, 0));// перемещение вдоль стороны
        }
        set_animator(speed_run);

        if (_charController.isGrounded)
            jump = false;

            //Поворот камеры
            if (transform.position.z > 2.4f && Mathf.Round(transform.eulerAngles.y) == 0 && side == "left")
        { // переход на правую сторону            
            side = "right";
            //редактируем позицию героя
            transform.position = new Vector3(transform.position.x, transform.position.y, 2.5f);
            
            //меняем положение камеры
            camera.transform.position=new Vector3(1.5f, 2.85f, 7);
            camera.transform.localEulerAngles = new Vector3(10, -170, 0);
        }
        else if (transform.position.x> 2.4f && Mathf.Round(Mathf.Abs(transform.eulerAngles.y))== 90 && side == "right")
        {// переход на левую сторону
            side = "left";
            //редактируем позицию героя
            transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
            
            //меняем положение камеры
            camera.transform.position = new Vector3(7, 2.85f, 1.5f);
            camera.transform.localEulerAngles = new Vector3(10, -100, 0);
        }
    }
}