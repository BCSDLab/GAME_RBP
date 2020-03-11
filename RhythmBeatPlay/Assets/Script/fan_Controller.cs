using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fan_Controller : MonoBehaviour
{
    /*
     * 다음의 링크의 도움을 압도적으로 받음.
     * https://stackoverflow.com/questions/44236320/drag-different-object-simultaneously-with-multiple-finger
    */

    public GameObject[] grabbed = new GameObject[5];
    private Vector2 movement = new Vector2();
    
    int num_object = 0;
    private void Update()
    {
        int nbTouches = Input.touchCount;

        if(nbTouches > 0)
        {
            for (int i = 0; i < nbTouches; i++)
            {
                Touch myTouch = Input.GetTouch(i);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
                Vector2 touchPos2D = new Vector2(touchPos.x, touchPos.y);
                Vector2 dir = Vector2.zero;
                RaycastHit2D hit = Physics2D.Raycast(touchPos2D,Camera.main.transform.forward);
                if (myTouch.phase == TouchPhase.Began && hit)
                {
                    if((hit.transform.gameObject.tag == "Blue_Hitpoint" || hit.transform.gameObject.tag == "Red_Hitpoint"))
                    {
                        nbTouches = Input.touchCount;
                        grabbed[i+1] = grabbed[i];
                        grabbed[i] = hit.transform.gameObject;
                        num_object++;
                        Debug.Log("began" + nbTouches);
                    }
                }
                if ((myTouch.phase == TouchPhase.Moved || myTouch.phase == TouchPhase.Stationary )&& grabbed[i] != null && hit)
                {
                    if((hit.transform.gameObject.tag == "Blue_Hitpoint" || hit.transform.gameObject.tag == "Red_Hitpoint"))
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

    }
   
}
