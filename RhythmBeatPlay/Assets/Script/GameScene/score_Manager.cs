using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_Manager : MonoBehaviour
{
    public float score;
    public float score_Step;
    public int note_Count;

    public Text score_Text;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        note_Count = GameObject.Find("note_spawner").GetComponent<note_spawning>().totalNoteCount;
        score_Step = 1000000 / note_Count;
    }

    public void Increase_Score()
    {
        score += score_Step;
        score_Text.text = ((int)score).ToString();
    }
}
