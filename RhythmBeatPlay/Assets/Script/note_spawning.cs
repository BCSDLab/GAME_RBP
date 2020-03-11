using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class note_spawning : MonoBehaviour
{
    // 노트 경로
    string m_strPath = "Assets/Resources/Notedatas/";

    // 데이터 저장
    List<string[]> Data = new List<string[]>();
    List<note> NoteData = new List<note>();
    public string title;        // 타이틀
    public string artist;       // 아티스트
    public double bpm;          // BPM
    public int totalnoteCount;      // 노트 개수

    public List<note> noteList; // 노트 데이터 리스트
    int num_data_count;

    // 빨강, 파랑, 보라색 노트를 지정한 부분.
    public GameObject[] obj = new GameObject[3];

    private void Awake()
    {
        Parse();
        SetSongData();
        GetNoteData();
        num_data_count = 0;
        debug();
    }

    void SetSongData()
    {
        title = Data[0][0];
        artist = Data[1][0];
        bpm = float.Parse(Data[2][0]);
        totalnoteCount = int.Parse(Data[3][0]);
    }

    void GetNoteData()
    {
        for (int i = 5; i < Data.Capacity; i++)
        {
            NoteData.Add(new note(int.Parse(Data[i][0]), float.Parse(Data[i][1]), int.Parse(Data[i][2]), int.Parse(Data[i][3])));
        }
    }

    public void debug()
    {
        print("title = " + title);
        print("artist = " + artist);
        print("bpm = " + bpm);
        print("totalnoteCount = " + totalnoteCount);
        for (int i = 0; i < NoteData.Capacity - 1; i++)
        {
            print(NoteData[i].getBar() + " " + NoteData[i].getBarTime() + " " + NoteData[i].getDegree() + " " + NoteData[i].getType());
        }
    }

    // 파싱 작업.
    public void Parse()

    {
        TextAsset data = Resources.Load("Notedatas/song1", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다.
        string source = sr.ReadLine();
        string[] values;
        while (source != null)

        {
            values = source.Split(' ');  // 스페이스로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
            Data.Add(values);
            if (values.Length == 0)

            {
                sr.Close();

                return;
            }
            source = sr.ReadLine();    // 한줄 읽는다.

        }
    }

    public void noteSpawn(int _beatcount)
    {
        if (NoteData[num_data_count].getBar() == _beatcount)
        {
            Instantiate(obj[NoteData[num_data_count].getType()], new Vector3(0f, 5.0f, 0f), Quaternion.Euler(0, 0, NoteData[num_data_count].getDegree()));
            if (num_data_count < totalnoteCount)
            {
                num_data_count++;
            }
        }
    }

    public class note
    {
        int bar; // bpm의 마디를 의미.
        float after_bar_time; // bpm에 비례해, 해당 bar 이후 몇 bar 이후에 비트가 내려쳐질지.
        int degree; // 각도
        int type; // 종류

        public void setBar(int m_bar)
        {
            bar = m_bar;
        }
        public void setBarTime(float m_after_bar_time)
        {
            after_bar_time = m_after_bar_time;
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
        public float getBarTime()
        {
            return after_bar_time;
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
            after_bar_time = 0;
            degree = 0;
            type = 0;
        }
        public note(int m_bar, float m_after_bar_time, int m_degree, int m_type)
        {
            after_bar_time = m_after_bar_time;
            bar = m_bar;
            degree = m_degree;
            type = m_type;
        }
    }


}

