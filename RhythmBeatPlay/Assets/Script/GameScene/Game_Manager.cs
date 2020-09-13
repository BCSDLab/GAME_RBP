using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    public int note_died = 0;
    public Text Start_Counter;

    public string musicSelection = "82_BPM_Dubstep";
    public string musicDataName = "song1";
    public int stage_number = 0;
    
    public GameObject red_fan;
    public GameObject blue_fan;
    public GameObject pauseUI;

    public GameObject DataObjectHandled;
    public GameObject score_manager;


    private void Awake()
    {
        instance = this;
        red_fan = GameObject.Find("fan_red");
        blue_fan = GameObject.Find("fan_blue");
        DataObjectHandled = GameObject.Find("DataObject");
        stage_number = 0;
        try
        {
            stage_number = DataObjectHandled.GetComponent<DataManager>().stageNumber;
        }
        catch{}

        switch (stage_number)
        {
            case 0:
                musicSelection = "82_BPM_Dubstep";
                musicDataName = "song1";
                break;
            case 1:
                musicSelection = "108_BPM_Rainbow";
                musicDataName = "song2";
                break;
        }
    }

    private int temp_count = 0;

    private void Update()
    {
        if (note_died > note_spawner.GetComponent<note_spawning>().totalNoteCount)
        {
            Debug.Log("Song Ended");
        }
    }

    // 디버그용 함수.
    public void notemaking_debug()
    {
        print(note_died+6);
    }

    public bool is_pause = false; // 퍼즈 상태인지에 대한 값. 노트 update등에 사용됨.
    public bool is_pause_possible = false; // pause가 가능한 상태인지에 대해 확인해주는 값.
    public GameObject note_spawner;

    
    private void Start()
    {
        this.GetComponent<UI_Manager>().ReturnCount(); // 게임 시작 카운트를 시작.
        //StartCoroutine(SongCounter(note_spawner.GetComponent<note_spawning>().musicTime));
    }

    /*public IEnumerator SongCounter(float song_length)
    {
        if (musicplayedtime >= song_length + 3.0f)
        {
            Debug.Log("Music Ended");
        }
        if (is_pause == true)
        {
            song_length += Time.deltaTime;
            musicplayedtime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        else
        {
            musicplayedtime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }*/

    // 일시적인 사용의 pause.
    public void PauseButton()
    {
        if (is_pause_possible && !is_pause)
        {
            note_spawner.GetComponent<BPMcheck>().bgMusic.Pause();
            pauseUI.SetActive(true);
            is_pause = !is_pause;
        }
    }

    public void ResumeButton()
    {
        if (is_pause_possible)
        {
            StartCoroutine(PauseCounter());
        }
    }

    public IEnumerator PauseCounter()
    {
        pauseUI.SetActive(false);
        int start_int = 3;
        while (start_int > 0)
        {
            Start_Counter.text = start_int.ToString();
            start_int--;
            yield return new WaitForSeconds(1f);
        }
        Start_Counter.text = null;
        is_pause_possible = true;
        note_spawner.GetComponent<BPMcheck>().bgMusic.Play();
        is_pause = !is_pause;
    }

    public void ResultSceneLoad()
    {
        DataLoader.instance.UpdateScore();
        SceneManager.LoadScene("ResultScene", LoadSceneMode.Single);
    }
}