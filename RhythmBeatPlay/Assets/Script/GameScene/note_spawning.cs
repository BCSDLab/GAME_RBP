using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// 해당 부분을 프리팹과 게임 매니저로 옮기는게 맞는것 같음.
public class note_spawning : MonoBehaviour
{
    //MusicDataHolder Data = new MusicDataHolder();
    // 이 값이 음악 선택시에 넘어옴
    
    List<note> noteData = new List<note>();
    
    int num_data_count;
    int note_count;

    // 빨강, 파랑, 보라색 노트를 지정한 부분.
    public GameObject[] obj = new GameObject[3];

    private void Awake()
    {
        num_data_count = 0;
        //musicSelection = "82 BPM Dubstep2";
        Debug.Log("여기까진 되던데");
        noteData = GameObject.Find(Game_Manager.instance.musicSelection).GetComponent<MusicData>().GetNoteData();
        note_count = GameObject.Find(Game_Manager.instance.musicSelection).GetComponent<MusicData>().GetNoteCount();
    }

    public void noteSpawn(int _beatcount)
    {
        if (noteData[num_data_count].getBar() == _beatcount)
        {
            GameObject note = Instantiate(obj[noteData[num_data_count].getType()], transform.position, Quaternion.Euler(0, 0, noteData[num_data_count].getDegree()));
            note.transform.parent = this.transform;
            if (num_data_count < note_count)
            {
                num_data_count++;
            }
        }
    }
}

