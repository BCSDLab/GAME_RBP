using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicData : MonoBehaviour
{
    [SerializeField]
    AudioClip music;

    [SerializeField]
    string title;

    [SerializeField]
    string artist;

    [SerializeField]
    string album;

    [SerializeField]
    int noteCount;

    [SerializeField]
    int bpm;

    [SerializeField]
    int musicLengthSecond;

    // must be scaled 1:1
    [SerializeField]
    Sprite titleSprite;

    // must be scaled 16:9
    [SerializeField]
    Sprite backgroundSprite;

    [SerializeField]
    TextAsset noteData;
    

    //
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public int GetMusicLengthSecond()
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

    public TextAsset GetNoteData()
    {
        return noteData;
    }
}
