using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;
    float mouse_scroll_input;

    public Camera camera;

    public bool is_pause; // 정지 유무
    public bool is_playing_first_time; // 첫 노래 시작이 2초 뒤기 때문에, 2초뒤에 실행되게 만들어야 함. 정지 시 초기화.

    public int fixing_bit;
    public int note_add_angle;
    [SerializeField]
    private InputField input_angle;

    // Start is called before the first frame update
    void Start()
    {
        is_pause = true;
        is_playing_first_time = true;
    }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            mouse_scroll_input = Input.GetAxis("Mouse ScrollWheel");
            camera.transform.position += new Vector3(0, mouse_scroll_input * 200,0);
        }
    }

    public void ChangeEditBit(int bitcnt) // 비트 넘어갈때, 고치는 대상 바꿔주고, 고치는 대상 마크 지워주기
    {
        Debug.Log(bitcnt);
        EditorDataParser.instance.bits.GetChild(fixing_bit).transform.GetChild(3).gameObject.SetActive(false);
        fixing_bit = bitcnt;
    }

    public void StartButton()
    {
        if (is_playing_first_time)
        {
            EditorDataParser.instance.bgMusic.Play();
            StartCoroutine(FirstStartCounter());
            is_playing_first_time = false;
        }
        else
        {
            EditorDataParser.instance.bgMusic.UnPause();
            is_pause = false;
        }
        
    }

    IEnumerator FirstStartCounter()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        is_pause = false;
    }

    public void PauseButton()
    {
        EditorDataParser.instance.bgMusic.Pause();
        is_pause = true;
    }

    public void StopButton()
    {
        EditorDataParser.instance.BitsRemoval();
        EditorDataParser.instance.NoteGen();
        EditorDataParser.instance.bgMusic.Stop();
        is_pause = true;
        is_playing_first_time = true;
    }

    public void NoteReload()
    {
        EditorDataParser.instance.BitsRemoval();
        EditorDataParser.instance.Parse();
        EditorDataParser.instance.NoteGen();
        EditorDataParser.instance.bgMusic.Stop();
        is_pause = true;
        is_playing_first_time = true;
    }

    IEnumerator WaitParse()
    {
        yield return new WaitForSeconds(0.1f);
        
    }

    public void AddRedNote()
    {
        note_add_angle = int.Parse(input_angle.text);
        EditorDataParser.instance.AddNote(fixing_bit, 0, note_add_angle);
        NoteReload();
    }

    public void AddBlueNote()
    {
        note_add_angle = int.Parse(input_angle.text);
        EditorDataParser.instance.AddNote(fixing_bit, 1, note_add_angle);
        NoteReload();
    }
    public void AddPurpleNote()
    {
        note_add_angle = int.Parse(input_angle.text);
        EditorDataParser.instance.AddNote(fixing_bit, 2, note_add_angle);
        NoteReload();
    }
    public void DeleteNote()
    {
        EditorDataParser.instance.DeleteNote(fixing_bit);
        NoteReload();
    }
}
