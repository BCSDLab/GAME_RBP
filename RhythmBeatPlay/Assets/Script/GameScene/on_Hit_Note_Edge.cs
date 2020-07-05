using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class on_Hit_Note_Edge : MonoBehaviour
{
    public Text notecounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Red_Note" || collision.tag == "Blue_Note" || collision.tag == "Purple_Note")
        {
            Destroy(collision.gameObject);
            Game_Manager.instance.note_died++;
            notecounter.text = (Game_Manager.instance.note_died + 6).ToString();
        }
    }
}
