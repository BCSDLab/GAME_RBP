using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fan_Controller : MonoBehaviour
{
    private Vector2 movement = new Vector2();
    private int nbTouches = 0;
    private Vector3 targetPos;

    private void Update()
    {
        nbTouches = Input.touchCount;

        if (nbTouches > 0)
        {
            for (int i = 0; i < nbTouches; i++)
            {
                Touch touch = Input.GetTouch(i); // 터치의 i번째 입력값을 받아온다.
                Vector2 touchPos2D = Camera.main.ScreenToWorldPoint(Input.touches[i].position); // i번째 입력값의 2D상 좌표값
                RaycastHit2D rayHit = Physics2D.Raycast(touchPos2D, Camera.main.transform.forward); // 해당 지점에 Rayhit되었는지 확인.

                // 실제로 i번째 입력값 지점에 오브젝트가 있고, 그 오브젝트가 fan이 맞을 경우
                if (rayHit && (rayHit.transform.gameObject.tag == "Blue_Hitpoint" || rayHit.transform.gameObject.tag == "Red_Hitpoint"))
                {
                    GameObject recipient = rayHit.transform.gameObject;
                    // 터치가 움직이거나, 움직이지 않을 경우
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        Vector2 currentPosition = recipient.transform.position;
                        //Debug.Log("Moving " + nbTouches);
                        Vector2 moveTowards = Camera.main.ScreenToWorldPoint(touch.position);

                        // 해당 좌표지점으로 fan의 각도를 조절하는 부분.
                        movement = moveTowards - currentPosition;
                        movement.Normalize();
                        float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

                        // 빨강 fan이면 각도를 -90~0 도로, 파란 fan이면 각도를 0~90도로 제한.
                        if (targetAngle >= -85f && rayHit.transform.gameObject.tag == "Blue_Hitpoint")
                            recipient.transform.rotation = Quaternion.Euler(0, 0, targetAngle + 90);
                        else if (targetAngle <= -95f && rayHit.transform.gameObject.tag == "Red_Hitpoint")
                            recipient.transform.rotation = Quaternion.Euler(0, 0, targetAngle + 90);
                    }
                }
            }
        }
    }
}