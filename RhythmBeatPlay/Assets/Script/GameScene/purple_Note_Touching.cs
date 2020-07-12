using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Grade
{
    Miss = 0,
    Bad = 1,
    Normal = 2,
    Good = 3,
    Perfect = 4
}

public class purple_Note_Touching : MonoBehaviour
{
    //public GameObject FX_OnHitNote;
    private GameObject score_Manager;

    public bool red_triggered;
    public bool blue_triggered;

    static float note_halfsize = 10.0f;
    static float fan_halfsize = 63.2f;

    private void Awake()
    {
        score_Manager = GameObject.Find("ScoreManager");
        red_triggered = false;
        blue_triggered = false;
    }

    private void OnMouseDown() // 해당 노트에 터치가 먹혔을 경우 (180 스케일 (최소 판정 이상))
    {
        print("touch detected!");
        //Destroy(this.gameObject);
        if (this.transform.localScale.x >= 180 &&
           this.transform.rotation.z <= Game_Manager.instance.red_fan.transform.rotation.z + fan_halfsize - note_halfsize &&
           this.transform.rotation.z >= Game_Manager.instance.blue_fan.transform.rotation.z - fan_halfsize + note_halfsize)
        { // 해당 부분이 노트 트리거 부분을 대신하게 됨.
            float localScale = this.transform.localScale.x;
            Destroy(this.gameObject);
            Game_Manager.instance.note_died++;
            Calculate_Score(localScale);
        }
    }

    private void Calculate_Score(float _localScale) // 받아온 localscale에 따라 
    {
        if (_localScale <= 180)
        {
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Bad);
        }
        else if (_localScale <= 190)
        {
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Normal);
        }
        else if (_localScale <= 200)
        {
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Good);
        }
        else if (_localScale <= 205)
        {
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Perfect);
        }

    }

}