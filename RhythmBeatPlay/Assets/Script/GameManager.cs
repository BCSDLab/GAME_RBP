using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool is_pause = false;
    public GameObject note_spawner;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<UI_Manager>().ReturnCount(); // 게임 시작 카운트를 시작.
        StartCoroutine(SongCounter(note_spawner.GetComponent<note_spawning>().musicTime));
    }

    public IEnumerator SongCounter(float song_length)
    {
        yield return new WaitForSeconds(song_length + 3.0f);
        Debug.Log("music Ended");
    }

    // 일시적인 사용의 pause.
    public void Pause()
    {
        if (is_pause == false)
        {
            Time.timeScale = 0.0f;
            note_spawner.GetComponent<BPMcheck>().bgMusic.Pause();
        }
        else
        {
            Time.timeScale = 1.0f;
            note_spawner.GetComponent<BPMcheck>().bgMusic.Play();
        }
        is_pause = !is_pause;
    }
}
