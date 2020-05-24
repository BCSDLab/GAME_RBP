using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    GameObject pause_ui;
    public int note_died = 0;
    public Text Start_Counter;

    private void Awake()
    {
        instance = this;
        pause_ui = GameObject.Find("PauseUI");
        pause_ui.SetActive(false);
    }

    int temp_count = 0;
    private void Update()
    {
        if(note_died >= note_spawner.GetComponent<note_spawning>().totalNoteCount)
        {
            Debug.Log("Song Ended");
        }
    }

    public bool is_pause = false; // 퍼즈 상태인지에 대한 값. 노트 update등에 사용됨.
    public bool is_pause_possible = false; // pause가 가능한 상태인지에 대해 확인해주는 값.
    public GameObject note_spawner;
    
    void Start()
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
    public void Pause()
    {
        if (is_pause_possible)
        {
            if (is_pause == false)
            {
                note_spawner.GetComponent<BPMcheck>().bgMusic.Pause();
                pause_ui.SetActive(true);
                is_pause = !is_pause;
            }
            else
            {
                StartCoroutine(PauseCounter());
            }
            
        }
    }

    public IEnumerator PauseCounter()
    {
        pause_ui.SetActive(false);
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
}


