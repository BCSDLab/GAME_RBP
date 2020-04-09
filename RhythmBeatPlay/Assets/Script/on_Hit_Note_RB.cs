using UnityEngine;

/*
    빨강 / 파랑 노트를 잡는 코드. 이 스크립트는 fan_blue와 fan_red의 hitpoint에 연결되어있슴다.  
*/
public class on_Hit_Note_RB : MonoBehaviour
{
    public GameObject FX_OnHitNote;
    public GameObject score_Manager;

    void Start()
    {
        score_Manager = GameObject.Find("ScoreManager");
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "Blue_Hitpoint" && collision.gameObject.tag == "Blue_Note" ||
           this.tag == "Red_Hitpoint" && collision.gameObject.tag == "Red_Note")
        {
            GameObject Fx_hitnote_clone = Instantiate(FX_OnHitNote, new Vector3(0,5,0), collision.transform.rotation );
            score_Manager.GetComponent<score_Manager>().Increase_Score();
            Destroy(Fx_hitnote_clone, 0.3f);
            Destroy(collision.gameObject);
        }
    }
}