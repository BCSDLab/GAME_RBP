using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int number;
    public AudioClip preview;
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
        
    }
    public void init(int num, Vector3 position)
    {
        number = num;
        transform.position = position;
        name = "Stage" + number.ToString();
    }
}
