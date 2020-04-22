using System.Collections;
using System.Collections.Generic;
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
        this.GetComponent<Game_Manager>().note_spawner.GetComponent<BPMcheck>().MusicStartPause();
    }
}
