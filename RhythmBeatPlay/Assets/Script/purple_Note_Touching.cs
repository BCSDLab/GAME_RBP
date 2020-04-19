using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purple_Note_Touching : MonoBehaviour
{
    public GameObject FX_OnHitNote;
    public GameObject score_Manager;

    public bool red_triggered;
    public bool blue_triggered;

    // Start is called before the first frame update
    void Start()
    {
        score_Manager = GameObject.Find("GameManager");
        red_triggered = false;
        blue_triggered = false;
    }

    private void OnMouseDown()
    {
        if(red_triggered && blue_triggered)
        {
            Destroy(this.gameObject);
            score_Manager.GetComponent<score_Manager>().Increase_Score();
            GameObject Fx_hitnote_clone = Instantiate(FX_OnHitNote, new Vector3(0, 5, 0), this.transform.rotation);
        }
    }
}
