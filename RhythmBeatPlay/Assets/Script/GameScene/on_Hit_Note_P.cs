using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    보라 노트를 잡는 코드. fan_blue와 fan_red의 purplehitpoint에 각각 1개씩 들어갑니다.
*/
public class on_Hit_Note_P : MonoBehaviour
{
    public GameObject follow_fan;
    float fan_angle;
    bool is_Hitpoint_Red;
    
    private void Awake()
    {
        // 히트포인트가 빨강 부분에 종속되었는가 아닌가에 대한 확인.
        is_Hitpoint_Red = (this.tag == "Purple_Hitpoint_Redside");
    }

    // fan을 따라다니도록 만든 코드.
    void Update()
    {
        fan_angle = follow_fan.transform.eulerAngles.z + 73.63f;
        this.transform.rotation = Quaternion.Euler(0,0,fan_angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Purple_Note")
        {
            if (is_Hitpoint_Red)
            {
                collision.GetComponent<purple_Note_Touching>().red_triggered = true;
                Debug.Log("red triggered");
            }
            else
            {
                collision.GetComponent<purple_Note_Touching>().blue_triggered = true;
                Debug.Log("blue triggered");
            }
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Purple_Note")
        {
            if (is_Hitpoint_Red)
                collision.GetComponent<purple_Note_Touching>().untrigger_red();
            else
                collision.GetComponent<purple_Note_Touching>().untrigger_blue();
        }
    }*/
}
