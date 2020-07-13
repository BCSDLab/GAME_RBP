using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 해당 부분을 프리팹과 게임 매니저로 옮기는게 맞는것 같음.
public class note_spawning : MonoBehaviour
{
    // 노트 경로
    private string m_strPath = "Assets/Resources/Notedatas/";

    // 데이터 저장
    private List<string[]> data = new List<string[]>();

    private List<note> noteData = new List<note>();
    public string title;        // 타이틀
    public string artist;       // 아티스트
    public double bpm;          // BPM
    public double rbpm;         // 실질 게임에 적용되는 bpm
    public int totalNoteCount;      // 노트 개수
    public int divCount;       // 한 bpm를 몇마디로 나누어야 하는지.
    public float musicTime;

    public List<note> noteList; // 노트 데이터 리스트
    private int num_data_count;

    // 빨강, 파랑, 보라색 노트를 지정한 부분.
    public GameObject[] obj = new GameObject[3];

    private void Awake()
    {
        num_data_count = 0;
        //debug();
    }

    private void SetSongData()
    {
        title = data[0][0];
        artist = data[1][0];
        bpm = float.Parse(data[2][0]);
        totalNoteCount = int.Parse(data[3][0]);
        divCount = int.Parse(data[4][0]);
        rbpm = bpm * divCount;
        this.GetComponent<BPMcheck>().bpm = rbpm;
        this.GetComponent<BPMcheck>().bgMusic = GameObject.Find(title).GetComponent<AudioSource>();
        musicTime = this.GetComponent<BPMcheck>().bgMusic.clip.length;
    }

    private void GetNoteData()
    {
        for (int i = 6; i < totalNoteCount + 6; i++)
        {
            noteData.Add(new note(int.Parse(data[i][0]), int.Parse(data[i][1]), int.Parse(data[i][2])));
        }
    }

    public void debug()
    {
        print("title = " + title);
        print("artist = " + artist);
        print("bpm = " + bpm);
        print("totalNoteCount = " + totalNoteCount);
        for (int i = 6; i < data.Count - 1; i++)
        {
            print(i + " " + data[i][0] + " " + data[i][1] + " " + data[i][2]);
        }
    }

    // 파싱 작업.
    public void Parse()
    {
        TextAsset parseData = Resources.Load("Notedatas/song2", typeof(TextAsset)) as TextAsset;
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
        //musicSelection = "82 BPM Dubstep2";
        Debug.Log("여기까진 되던데");
        // noteData = GameObject.Find(Game_Manager.instance.musicSelection).GetComponent<MusicData>().GetNoteData();
        totalNoteCount = GameObject.Find(Game_Manager.instance.musicSelection).GetComponent<MusicData>().GetNoteCount();
    }

    public void noteSpawn(int _beatcount)
    {
        if (noteData[num_data_count].getBar() == _beatcount)
        {
            GameObject note = Instantiate(obj[noteData[num_data_count].getType()], transform.position, Quaternion.Euler(0, 0, noteData[num_data_count].getDegree()));
            note.transform.parent = this.transform;
            if (num_data_count < totalNoteCount)
            {
                num_data_count++;
            }
        }
    }

    public class note
    {
        private int bar; // bpm의 마디를 의미.
        private int degree; // 각도
        private int type; // 종류

        public void setBar(int m_bar)
        {
            bar = m_bar;
        }

        public void setDegree(int m_degree)
        {
            degree = m_degree;
        }

        public void setType(int m_type)
        {
            type = m_type;
        }

        public int getBar()
        {
            return bar;
        }

        public int getDegree()
        {
            return degree;
        }

        public int getType()
        {
            return type;
        }

        // constructor
        public note()
        {
            bar = 0;
            degree = 0;
            type = 0;
        }

        public note(int m_bar, int m_degree, int m_type)
        {
            bar = m_bar;
            degree = m_degree;
            type = m_type;
        }
    }
}
