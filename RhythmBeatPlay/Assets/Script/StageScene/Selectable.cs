using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public int number;
    public string objectName;
    public AudioClip preview;
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
        
    }
    public void init(int num, Vector3 position)
    {
        number = num;
        transform.position = position;
        name = "Object" + number.ToString();
    }
}
