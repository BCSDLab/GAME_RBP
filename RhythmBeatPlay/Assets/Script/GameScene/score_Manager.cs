using UnityEngine;
using UnityEngine.UI;

public class score_Manager : MonoBehaviour
{
    public float score;
    public float score_Step;
    public int note_Count;

    public int combo_Count; // 콤보 카운터


    public Text score_Text; // 점수 텍스트 제작.

    // Start is called before the first frame update
    private void Start()
    {
        score = 0;
        combo_Count = 0;
        note_Count = GameObject.Find(Game_Manager.instance.musicSelection).GetComponent<MusicData>().GetNoteCount();
        score_Step = 1000000 / note_Count;
    }

    // 관련 작업 수행 필요.
    public void Increase_Score(bool Is_Purple, int Grade)
    {
        float grade_Scale = 0.0f; 

        switch (Grade) // 받아온 점수에 대한 콤보카운트.
        {
            case 0:
                grade_Scale = 0.0f;
                combo_Count = 0;
                Debug.Log("Miss");
                break;
            case 1:
                grade_Scale = 0.4f;
                combo_Count++;
                Debug.Log("Bad");
                break;
            case 2:
                grade_Scale = 0.6f;
                combo_Count++;
                Debug.Log("Normal");
                break;
            case 3:
                grade_Scale = 0.8f;
                combo_Count++;
                Debug.Log("Good");
                break;
            case 4:
                grade_Scale = 1.0f;
                combo_Count++;
                Debug.Log("Perfect");
                break;
        }

        // 노트 1개당 점수 = 900000 * 정확도 / 노트 개수 + 100000 * 2 * 콤보 / (최대콤보 * (최대콤보 - 1))
        score_Step = ((900000 * grade_Scale) / ((note_Count)) + ((100000 * 2 * combo_Count) / (note_Count * (note_Count - 1))));

        score += score_Step;
        score_Text.text = ((int)score).ToString();
    }
}