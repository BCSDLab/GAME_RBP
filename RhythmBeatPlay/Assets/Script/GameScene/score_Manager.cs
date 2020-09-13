using UnityEngine;
using UnityEngine.UI;

public class score_Manager : MonoBehaviour
{
    public static score_Manager instance;
    public float score;
    public float score_step;
    public int note_count;

    public int combo_count; // 콤보 카운터

    public GameObject note_spawner;
    public Text score_Text; // 점수 텍스트 제작.
    public int[] notedatas = new int[5];

    private void Awake()
    {
        instance = this;
        note_spawner = GameObject.Find("note_spawner");
    }

    // Start is called before the first frame update
    private void Start()
    {
        score = 0;
        combo_count = 0;
        //note_count = GameObject.Find(Game_Manager.instance.musicSelection).GetComponent<MusicData>().GetNoteCount();
        note_count = note_spawner.GetComponent<note_spawning>().totalNoteCount;
        score_step = 1000000 / note_count;

        for(int i = 0; i < 5; i++)
        {
            notedatas[i] = 0;
        }
    }

    // 관련 작업 수행 필요.
    public void Increase_Score(bool Is_Purple, int Grade)
    {
        float grade_Scale = 0.0f; 

        switch (Grade) // 받아온 점수에 대한 콤보카운트.
        {
            case 4:
                grade_Scale = 1.0f;
                combo_count++;
                notedatas[4]++;
                break;
            case 3:
                grade_Scale = 0.8f;
                combo_count++;
                notedatas[3]++;
                break;
            case 2:
                grade_Scale = 0.6f;
                combo_count++;
                notedatas[2]++;
                break;
            case 1:
                grade_Scale = 0.4f;
                combo_count++;
                notedatas[1]++;
                break;
            case 0:
                grade_Scale = 0.0f;
                if (combo_count > DataLoader.instance.max_combo)
                    DataLoader.instance.max_combo = combo_count;
                combo_count = 0;
                notedatas[0]++;
                break;
        }

        // 노트 1개당 점수 = 900000 * 정확도 / 노트 개수 + 100000 * 2 * 콤보 / (최대콤보 * (최대콤보 - 1))
        score_step = ((900000 * grade_Scale) / ((note_count)) + ((100000 * 2 * combo_count) / (note_count * (note_count - 1))));

        score += score_step;
        score_Text.text = ((int)score).ToString();
    }
}