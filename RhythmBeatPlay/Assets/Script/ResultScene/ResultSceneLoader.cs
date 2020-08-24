using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneLoader : MonoBehaviour
{
    [SerializeField]
    private Text[] resultnote = new Text[5];
    [SerializeField]
    private Text scoretext;
    [SerializeField]
    private Text songtext;

    [SerializeField]
    private Text difficultytext;
    [SerializeField]
    private Text resultranktext;
    [SerializeField]
    private Text maxcombotext;

    public Sprite[] difficultynotelist = new Sprite[3];
    public Image difficultyimage;

    public int temp_difficulty_getting;
    public int[] temp_noteresult = new int[5];

    public GameObject dataloader;

    private void Awake()
    {
        if (GameObject.Find("DataLoader"))
        {
            dataloader = GameObject.Find("DataLoader");
        }
        else
        {
            Debug.Log("정상적으로 데이터로더를 찾지 못함.");
        }

        for (int i = 0; i < 5; i++)
        {
            resultnote[i] = GameObject.Find("NoteResults").transform.GetChild(i).GetComponent<Text>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        // 점수
        scoretext.text = Convert.ToInt32(DataLoader.instance.score).ToString();
        // 각 구간별 수
        for (int i = 0; i < 5; i++)
        {
            resultnote[i].text = "                " + DataLoader.instance.notedatas[i].ToString();
        }

        // 음악 제목
        switch (DataLoader.instance.musicnum)
        {
            case 0:
                songtext.text = "Dubstep";
                break;
            case 1:
                songtext.text = "Rainbow";
                break;
            case 2:
                songtext.text = "";
                break;
        }

        // 난이도
        switch (DataLoader.instance.difficultynum)
        {
            case 0:
                difficultytext.text = "Easy";
                break;
            case 1:
                difficultytext.text = "Normal";
                break;
            case 2:
                difficultytext.text = "Hard";
                break;
        }

        // 랭크
        if (DataLoader.instance.score > 900000)
            resultranktext.text = "S";
        else if (DataLoader.instance.score > 800000)
            resultranktext.text = "A";
        else if (DataLoader.instance.score > 650000)
            resultranktext.text = "B";
        else if (DataLoader.instance.score > 500000)
            resultranktext.text = "C";
        else
            resultranktext.text = "D";

        // 최대 콤보
        maxcombotext.text = "Max Combo\n" + DataLoader.instance.max_combo.ToString();
    }

    public void LoadMusicSelectScene()
    {
        Debug.Log("Pressed!");
        SceneManager.LoadScene("MusicSelectScene", LoadSceneMode.Single);
    }
}
