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
    public GameObject[] hit_particles = new GameObject[5];

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
            degree_Difference = 2 * Mathf.Rad2Deg * Mathf.Abs(blue_Fan.transform.rotation.z - collision.gameObject.transform.rotation.z);
            Calculate_Score(collision);
            Destroy(collision.gameObject);
        }
        else if(this.tag == "Red_Hitpoint" && collision.gameObject.tag == "Red_Note")
        {
            degree_Difference = 2 * Mathf.Rad2Deg * Mathf.Abs(red_Fan.transform.rotation.z - collision.gameObject.transform.rotation.z);
            Calculate_Score(collision);
            Destroy(collision.gameObject);
        }
    }

    private void Calculate_Score(Collider2D collision) // Sending judeged note_degrees.
    {
        // Is_Purple에서 보라색이 아니므로 0, 단계에 맞는 Increase_score grade를 주면 된다.
        if (degree_Difference >= 8.0) // Bad
        {
            GameObject Particle = Instantiate(hit_particles[(int)Grade.Bad], new Vector3(0,540,0), collision.transform.rotation);
            Particle.transform.localScale = new Vector3(0.98f, 0.98f, 1);
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Bad); 
        }
        else if(degree_Difference >= 6.0) // Normal
        {
            GameObject Particle = Instantiate(hit_particles[(int)Grade.Normal], new Vector3(0,540,0), collision.transform.rotation);
            Particle.transform.localScale = new Vector3(0.98f, 0.98f, 1);
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Normal); 
        }
        else if(degree_Difference >= 3.0) // Good
        {
            GameObject Particle = Instantiate(hit_particles[(int)Grade.Good], new Vector3(0,540,0), collision.transform.rotation);
            Particle.transform.localScale = new Vector3(0.98f, 0.98f, 1);
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Good);
        }
        else // Perfect
        {
            GameObject Particle = Instantiate(hit_particles[(int)Grade.Perfect], new Vector3(0,540,0), collision.transform.rotation);
            Particle.transform.localScale = new Vector3(0.98f, 0.98f, 1);
            score_Manager.GetComponent<score_Manager>().Increase_Score(false, (int)Grade.Perfect);
        }
        Game_Manager.instance.note_died++;
    }
}