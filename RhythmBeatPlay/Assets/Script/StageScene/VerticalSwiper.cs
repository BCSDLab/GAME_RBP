using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class VerticalSwiper : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float activeDistance = 150f;
    public float activeTime = 0.5f;
    public UnityEvent OnDragLeft;
    public UnityEvent OnDragRight;
    private Vector3 begin;
    private bool down = false;
    private float downTime = 0;

    void Update()
    {
        if (down)
        {
            downTime += Time.deltaTime;
            if (downTime > activeTime)
                down = false;
        }
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        begin = eventData.position;
        down = true;
        downTime = 0f;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        down = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (down)
        {
            if (begin.x - eventData.position.x > activeDistance)
            {
                OnDragLeft.Invoke();
                down = false;
            }
            else if (begin.x - eventData.position.x < -activeDistance)
            {
                OnDragRight.Invoke();
                down = false;
            }
        }
    }
}
