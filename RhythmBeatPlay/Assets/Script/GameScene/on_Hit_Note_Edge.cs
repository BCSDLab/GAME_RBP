using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_Hit_Note_Edge : MonoBehaviour
{
    public int note_died = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Red_Note" || collision.tag == "Blue_Note" || collision.tag == "Purple_Note")
        {
            Destroy(collision.gameObject);
            note_died++;
            Debug.Log(++note_died);
            Debug.Log(System.DateTime.Now.ToString());
        }
    }
}
