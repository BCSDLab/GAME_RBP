using UnityEngine;

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
    private int musicLengthSecond;

    // must be scaled 1:1
    [SerializeField]
    private Sprite titleSprite;

    // must be scaled 16:9
    [SerializeField]
    private Sprite backgroundSprite;

    [SerializeField]
    private TextAsset noteData;

    //
    private void Awake()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
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