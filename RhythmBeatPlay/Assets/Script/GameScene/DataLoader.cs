using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;
    public int musicnum; // 음악의 넘겨받은 코드
    public string musicname; // 음악 제목

    public int difficultynum; // 난이도
    public float score; // 점수\
    public float max_combo;
    public int[] notedatas = new int[5]; // 노트별 데이터

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        musicnum = Game_Manager.instance.stage_number;
        musicname = Game_Manager.instance.musicSelection;

        // 아직 난이도 관련 요소가 없음.
        difficultynum = 0;
        max_combo = 0;
    }


    public void UpdateScore()
    {
        score = score_Manager.instance.score;
        notedatas = score_Manager.instance.notedatas;
    }
}
