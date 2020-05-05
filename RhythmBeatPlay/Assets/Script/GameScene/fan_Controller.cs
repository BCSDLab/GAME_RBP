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