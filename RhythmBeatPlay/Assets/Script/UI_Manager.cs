using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text Start_Text;

    public void ReturnCount()
    {
        StartCoroutine(ReturnCount_IE());
    }

    public IEnumerator ReturnCount_IE()
    {
        int start_int = 3;
        while (start_int > 0)
        {
            Start_Text.text = start_int.ToString();
            start_int--;
            yield return new WaitForSeconds(1f);
        }
        Start_Text.text = null;
        this.GetComponent<GameManager>().note_spawner.GetComponent<BPMcheck>().MusicStartPause();
    }
}
