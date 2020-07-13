
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MusicData : MonoBehaviour
{
    [SerializeField]
    private AudioClip music;

    [SerializeField]
    private string title;

    [SerializeField]
    private string artist;

    [SerializeField]
    private string album;

    [SerializeField]
    private int noteCount;

    [SerializeField]
    private int bpm;

    [SerializeField]
    float musicLengthSecond;

    // must be scaled 1:1
    [SerializeField]
    private Sprite titleSprite;

    // must be scaled 16:9
    [SerializeField]
    private Sprite backgroundSprite;

    List<note> noteData = new List<note>();


    private void Awake()
    {
        Parse();
    }

    public void Parse()
    {
        List<string[]> data = new List<string[]>();

        TextAsset parseData = Resources.Load("
        
        s/song1", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(parseData.text);

        // 먼저 한줄을 읽는다.
        string source = sr.ReadLine();
        string[] values;
        while (source != null)
        {
            values = source.Split(' ');  // 스페이스로 구분한다.
            data.Add(values);
            if (values.Length == 0)
            {
                sr.Close();
                return;
            }
            source = sr.ReadLine();    // 한줄 읽는다.
        }

        // SetSongData
        title = data[0][0];
        artist = data[1][0];
        Debug.Log("bpm" + int.Parse(data[2][0]));
        Debug.Log("rate" + int.Parse(data[4][0]));
        bpm = (int.Parse(data[2][0]) * int.Parse(data[4][0]));
        noteCount = int.Parse(data[3][0]);

        // 음악 설정
        GameObject.Find("note_spawner").GetComponent<BPMcheck>().bpm = bpm;
        GameObject.Find("note_spawner").GetComponent<BPMcheck>().bgMusic = GameObject.Find(title).GetComponent<AudioSource>();
        musicLengthSecond = GameObject.Find("note_spawner").GetComponent<BPMcheck>().bgMusic.clip.length;

        // SetNoteData
        for (int i = 6; i < noteCount + 6; i++)
        {
            noteData.Add(new note(int.Parse(data[i][0]), int.Parse(data[i][1]), int.Parse(data[i][2])));
        }
         // 데이터는 모두 정상적으로 들어감을 확인함.
    }

    public AudioClip GetAudioClip()
    {
        return music;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetArtist()
    {
        return artist;
    }

    public string GetAlbum()
    {
        return album;
    }

    public int GetNoteCount()
    {
        return noteCount;
    }

    public int GetBPM()
    {
        return bpm;
    }

    public float GetMusicLengthSecond()
    {
        return musicLengthSecond;
    }

    public Sprite GetTitleSprite()
    {
        return titleSprite;
    }

    public Sprite GetBackgroundSprite()
    {
        return backgroundSprite;
    }

    public List<note> GetNoteData()
    {
        return noteData;
    }
}