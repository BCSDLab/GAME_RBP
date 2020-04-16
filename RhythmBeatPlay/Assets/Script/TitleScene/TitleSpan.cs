using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSpan : MonoBehaviour
{
    [SerializeField]
    GameObject spanRed;

    [SerializeField]
    GameObject spanBlue;

    float speed = 1.0f;
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */

    // Update is called once per frame
    void Update()
    {
        spanBlue.transform.Rotate(new Vector3(0,0, 1 * speed));
        spanRed.transform.Rotate(new Vector3(0, 0, -1.5f * speed));
    }
}
