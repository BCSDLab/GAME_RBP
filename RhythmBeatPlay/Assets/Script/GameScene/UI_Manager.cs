using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text Start_Counter;

    public Slider progress_bar;

    private void Start()
    {
        StartCoroutine(ProgressBarMoving(note_spawning.instance.musicTime));
        progress_bar.value = 0;
    }
    private void Update()
    {
            
    }

    public void ReturnCount()
    {
        StartCoroutine(ReturnCount_IE());
    }

    public IEnumerator ReturnCount_IE()
    {
        int start_int = 3;
        while (start_int > 0)
        {
            Start_Counter.text = start_int.ToString();
            start_int--;
            yield return new WaitForSeconds(1f);
        }
        Start_Counter.text = null;
        // pause 가 카운팅 이후로부터 가능하도록 함.
        Game_Manager.instance.is_pause_possible = true;
        this.GetComponent<Game_Manager>().note_spawner.GetComponent<BPMcheck>().MusicStartPause();
    }

    public IEnumerator ProgressBarMoving(float _songtime)
    {
        Debug.Log("Song Time is : " + _songtime);
        float degreepersec =  1.0f / _songtime;
        float songtime = _songtime;
        while(songtime > 0 && !Game_Manager.instance.is_pause)
        {
            Debug.Log(_songtime - songtime);
            yield return new WaitForSeconds(1.0f);
            progress_bar.value += degreepersec;
            songtime--;
        }
        Debug.Log("MUsic ENDED!");
        Game_Manager.instance.ResultSceneLoad();
    }
}