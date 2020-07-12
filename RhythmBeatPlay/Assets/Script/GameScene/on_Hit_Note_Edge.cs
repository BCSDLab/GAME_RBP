using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_Hit_Note_Edge : MonoBehaviour
{
    private GameObject score_Manager;
    

    private void Awake()
    {
        score_Manager = GameObject.Find("ScoreManager");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Red_Note" || collision.tag == "Blue_Note" || collision.tag == "Purple_Note")
        {
            Destroy(collision.gameObject);
            score_Manager.GetComponent<score_Manager>().Increase_Score(true, 0);
            Game_Manager.instance.note_died++;
        }
    }
}
