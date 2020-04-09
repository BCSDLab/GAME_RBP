using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fan_Controller : MonoBehaviour
{
    /*
     * 다음의 링크의 도움을 압도적으로 받음.
     * https://stackoverflow.com/questions/44236320/drag-different-object-simultaneously-with-multiple-finger
    */

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
                Touch touch = Input.GetTouch(i);
                Vector2 touchPos2D = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
                RaycastHit2D rayHit = Physics2D.Raycast(touchPos2D, Camera.main.transform.forward);

                if (rayHit && (rayHit.transform.gameObject.tag == "Blue_Hitpoint" || rayHit.transform.gameObject.tag == "Red_Hitpoint"))
                {
                    GameObject recipient = rayHit.transform.gameObject;
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        Vector2 currentPosition = recipient.transform.position;
                        Debug.Log("Moving " + nbTouches);
                        Vector2 moveTowards = Camera.main.ScreenToWorldPoint(touch.position);

                        movement = moveTowards - currentPosition;
                        movement.Normalize();
                        float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                        recipient.transform.rotation = Quaternion.Euler(0, 0, targetAngle + 90);
                    }
                }
            }
        }
    }
}

// 이전 버전의 오류있던 코드. 혹시 몰라서 넣음.
/*
 private void FixedUpdate()
 {
     nbTouches = Input.touchCount;
     if(nbTouches > 0)
     {
         for (int i = 0; i < nbTouches; i++)
         {
             Touch myTouch = Input.GetTouch(i);
             Vector2 touchPos2D = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
             //Vector2 touchPos2D = new Vector2(touchPos.x, touchPos.y);
             RaycastHit2D rayHit = Physics2D.Raycast(touchPos2D,Camera.main.transform.forward);
             if (myTouch.phase == TouchPhase.Began && rayHit)
             {
                 if((rayHit.transform.gameObject.tag == "Blue_Hitpoint" || rayHit.transform.gameObject.tag == "Red_Hitpoint"))
                 {
                     nbTouches = Input.touchCount;
                     grabbed[i+1] = grabbed[i];
                     grabbed[i] = rayHit.transform.gameObject;
                     num_object++;
                     Debug.Log("began" + nbTouches);
                 }
             }
             if ((myTouch.phase == TouchPhase.Moved || myTouch.phase == TouchPhase.Stationary )&& grabbed[i] != null && rayHit)
             {
                 if((rayHit.transform.gameObject.tag == "Blue_Hitpoint" || rayHit.transform.gameObject.tag == "Red_Hitpoint"))
                 {
                     Vector2 currentPosition = grabbed[i].transform.position;
                     Debug.Log("Moving " + nbTouches);
                     Vector2 moveTowards = Camera.main.ScreenToWorldPoint(myTouch.position);

                     movement = moveTowards - currentPosition;
                     movement.Normalize();
                     float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                     grabbed[i].transform.rotation = Quaternion.Euler(0, 0, targetAngle + 90);
                 }
             }
             if (myTouch.phase == TouchPhase.Ended)
             {
                 grabbed[i] = null;
                 Debug.Log("ended" + nbTouches);
             }
             if (i > 0 && grabbed[i - 1] == null && grabbed[i] != null)
             {
                 grabbed[i - 1] = grabbed[i];
                 grabbed[i] = null;
             }
         }
     }
     // Debug.Log("now" + num_object + nbTouches);
 }*/
