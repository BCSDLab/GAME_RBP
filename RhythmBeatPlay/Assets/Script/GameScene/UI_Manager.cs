using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text Start_Counter;

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
}