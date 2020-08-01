using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class on_Hit_Note_Edge : MonoBehaviour
{
    private GameObject score_Manager;
    public GameObject miss_particle;

    private void Awake()
    {
        score_Manager = GameObject.Find("ScoreManager");
    }
    //public Text notecounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Red_Note" || collision.tag == "Blue_Note")
        {
            Destroy(collision.gameObject);
            Instantiate(miss_particle, new Vector3(0,540,0), collision.transform.rotation);
            score_Manager.GetComponent<score_Manager>().Increase_Score(true, 0);
            Game_Manager.instance.note_died++;
            //notecounter.text = (Game_Manager.instance.note_died + 6).ToString();
        }
        else if(collision.tag == "Purple_Hitpoint")
        {
            Destroy(collision.transform.parent.gameObject);
            Instantiate(miss_particle, new Vector3(0,540,0), collision.transform.parent.rotation);
            score_Manager.GetComponent<score_Manager>().Increase_Score(true, 0);
            Game_Manager.instance.note_died++;
            //notecounter.text = (Game_Manager.instance.note_died + 6).ToString();
        }
    }
}
