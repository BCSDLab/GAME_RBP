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

    // 점수 업 (추후 콤보 수 등을 고려할 예정.)
    public void Increase_Score()
    {
        Debug.Log("increase score");
        score += score_Step;
        score_Text.text = ((int)score).ToString();
    }
}
