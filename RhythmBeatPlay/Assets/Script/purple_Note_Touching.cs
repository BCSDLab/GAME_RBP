using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purple_Note_Touching : MonoBehaviour
{
    bool red_triggered;
    bool blue_triggered;

    // Start is called before the first frame update
    void Start()
    {
        red_triggered = false;
        blue_triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(red_triggered && blue_triggered)
        {
            Destroy(this.gameObject);
        }
    }

    public void trigger_red()
    {
        red_triggered = true;
        Debug.Log("red triggered");
    }

    public void trigger_blue()
    {
        blue_triggered = true;
        Debug.Log("blue triggered");
    }

    public void untrigger_red()
    {
        red_triggered = false;
    }

    public void untrigger_blue()
    {
        blue_triggered = false;
    }
}
