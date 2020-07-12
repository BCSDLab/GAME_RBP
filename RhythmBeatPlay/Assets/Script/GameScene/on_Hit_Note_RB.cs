using UnityEngine;

/*
    빨강 / 파랑 노트를 잡는 코드. 이 스크립트는 fan_blue와 fan_red의 hitpoint에 연결되어있슴다.
*/

public class on_Hit_Note_RB : MonoBehaviour
{
    //public GameObject FX_OnHitNote;
    GameObject score_Manager;
    GameObject blue_Fan;
    GameObject red_Fan;

    private float degree_Difference;

    enum Grade
    {
        Miss = 0,
        Bad = 1,
        Normal = 2,
        Good = 3,
        Perfect = 4
    }

    private void Awake() // ScoreManager 도 싱글톤으로 할지 고민해야할 듯.
    {
        score_Manager = GameObject.Find("ScoreManager");
        blue_Fan = GameObject.Find("fan_blue");
        red_Fan = GameObject.Find("fan_red");
    }
    

    private void OnTriggerEnter2D(Collider2D collision) // 짝이 맞는 노트 / 팬이 서로 만났을 경우.
    {
        if (this.tag == "Blue_Hitpoint" && collision.gameObject.tag == "Blue_Note")
        {
            degree_Difference = Mathf.Abs(blue_Fan.transform.rotation.z - collision.gameObject.transform.rotation.z);
            Calculate_Score();
            Destroy(collision.gameObject);
        }
        else if(this.tag == "Red_Hitpoint" && collision.gameObject.tag == "Red_Note")
        {
            degree_Difference = Mathf.Abs(blue_Fan.transform.rotation.z - collision.gameObject.transform.rotation.z);
            Calculate_Score();
            Destroy(collision.gameObject);
        }
    }

    private void Calculate_Score() // Sending judeged note_degrees.
    {
        
        Debug.Log(Mathf.Rad2Deg * degree_Difference);
        // Is_Purple에서 보라색이 아니므로 0, 단계에 맞는 Increase_score grade를 주면 된다.
        if (degree_Difference >= 8.0) // Bad
        {
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Bad); 
        }
        else if(degree_Difference >= 6.0) // Normal
        {
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Normal); 
        }
        else if(degree_Difference >= 3.0) // Good
        {
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Good);
        }
        else // Perfect
        {
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Perfect);
        }
        Game_Manager.instance.note_died++;
    }
}