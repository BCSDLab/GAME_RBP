using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class EditorDataParser : MonoBehaviour
{
    
    // 노트 경로
    public static EditorDataParser instance;
    private string m_strPath = "Assets/Resources/Notedatas/";

    // 데이터 저장
    private List<string[]> data = new List<string[]>();

    public List<note> noteData = new List<note>();
    public string songselection;
    public string title;        // 타이틀
    public string artist;       // 아티스트
    public double bpm;          // BPM
    public double rbpm;         // 실질 게임에 적용되는 bpm
    public int totalNoteCount;      // 노트 개수
    public int divCount;       // 한 bpm를 몇마디로 나누어야 하는지.
    public float musicTime;
    public AudioSource bgMusic;

    public int last_bit;
    private int num_data_count;
    public double secondperbeat;
    // 빨강, 파랑, 보라색 노트를 지정한 부분.
    public GameObject[] obj = new GameObject[3];
    public Transform bits;
    // 여기서부터는 노트 제작을 위한 데이터들임.
    public GameObject bit;
    public int notegen_bitcnt;

    
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
        songselection = "song1";
        num_data_count = 0;
        Parse();
        // 임시
        //AddNote(1,1);
        //debug();
    } 

    // 파싱 작업.
    public void Parse()
    {
        TextAsset parseData = Resources.Load("Notedatas/" + songselection, typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(parseData.text);
        data.Clear();
        noteData.Clear();
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

        title = data[0][0];
        artist = data[1][0];
        bpm = float.Parse(data[2][0]);
        totalNoteCount = int.Parse(data[3][0]);
        Debug.Log("totalnote" + totalNoteCount);
        divCount = int.Parse(data[4][0]);
        rbpm = bpm * divCount;
        secondperbeat = 1 / (rbpm / 60);
        bgMusic = GameObject.Find(title).GetComponent<AudioSource>();
        musicTime = bgMusic.clip.length;
        for (int i = 6; i < totalNoteCount + 6; i++)
        {
            Debug.Log(data[i][0] + " " + data[i][1] + " " + data[i][2]);
            noteData.Add(new note(int.Parse(data[i][0]), int.Parse(data[i][1]), int.Parse(data[i][2])));
        }
        last_bit = noteData[noteData.Count-1].getBar();
        //Debug.Log("Last Bit is" + last_bit);
    }
   
    public void AddNote(int bitcnt, int type, int angle) // 0 - 빨 , 1 - 파 , 2 - 보
    {
        File.Delete(m_strPath + songselection + ".txt");
        FileStream f = new FileStream(m_strPath + songselection + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
        //FileStream f = new FileStream(m_strPath + "song5.txt", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        for(int i = 0; i < 6; i++)
        {
            string temp_str = null;
            for (int j = 0; j < data[i].Length; j++)
            {
                if (i == 3 && j == 0)
                {
                    int notecnt = int.Parse(data[i][j]) + 1;
                    temp_str += notecnt.ToString();
                }
                else
                {
                    temp_str += data[i][j];
                }

            }
            writer.WriteLine(temp_str);
        } // 처음부분 데이터
        for (int i = 6; i < data.Count; i++)
        {
            bool added = false;
            string temp_str = null;
            for (int j = 0; j < data[i].Length; j++)
            {
                if(j == 0 && i+1 < data.Count && bitcnt >= int.Parse(data[i][j]) && bitcnt < int.Parse(data[i+1][j])){
                    string addnote_str = data[i][0] + " " + data[i][1] + " " + data[i][2];
                    writer.WriteLine(addnote_str);
                    addnote_str = bitcnt + " " + angle + " " + type;
                    writer.WriteLine(addnote_str);
                    added = true;
                    break;
                }
                else
                {
                    temp_str += data[i][j];
                    temp_str += " ";
                }
            }
            if (!added)
            {
                writer.WriteLine(temp_str);
            }
        }
        writer.Close();
        AssetDatabase.ImportAsset(m_strPath + songselection + ".txt");
    }

    public void DeleteNote(int bitcnt)
    {
        File.Delete(m_strPath + songselection + ".txt");
        FileStream f = new FileStream(m_strPath + songselection + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);

        for (int i = 0; i < 6; i++)
        {
            string temp_str = null;
            for (int j = 0; j < data[i].Length; j++)
            {
                if (i == 3 && j == 0)
                {
                    int notecnt = int.Parse(data[i][j]) - 1;
                    temp_str += notecnt.ToString();
                }
                else
                {
                    temp_str += data[i][j];
                }

            }
            writer.WriteLine(temp_str);
        } // 처음부분 데이터
        for (int i = 6; i < data.Count; i++)
        {
            bool added = false;
            string temp_str = null;
            for (int j = 0; j < data[i].Length; j++)
            {
                if (j == 0 && i + 1 < data.Count && bitcnt == int.Parse(data[i][j]) && bitcnt < int.Parse(data[i + 1][j]))
                {
                    string addnote_str = data[i+1][0] + " " + data[i+1][1] + " " + data[i+1][2];
                    writer.WriteLine(addnote_str);
                    i++;
                    added = true;
                    break;
                }
                else
                {
                    temp_str += data[i][j];
                    temp_str += " ";
                }
            }
            if (!added)
            {
                writer.WriteLine(temp_str);
            }
        }
        writer.Close();
        AssetDatabase.ImportAsset(m_strPath + songselection + ".txt");

    }

    private void Start()
    {
        NoteGen();
    }
    
    public void NoteGen()
    {
        notegen_bitcnt = 0; //  각 비트에 대한 for문 루프
        int gen_xaxis = 80;
        int gen_yaxis = -200;
        GameObject temp_bit;
        int bit_index = 0;
        for (int notegen_bitcnt = 0; notegen_bitcnt < last_bit; notegen_bitcnt++)
        {
            temp_bit = Instantiate(bit, new Vector3(gen_xaxis, gen_yaxis, 0), Quaternion.identity);
            temp_bit.GetComponent<BitMove>().bitcnt = notegen_bitcnt;
            gen_yaxis += 40;
            while (noteData[bit_index].getBar() == notegen_bitcnt)
            {
                temp_bit.GetComponent<BitMove>().AddNote(noteData[bit_index]);
                bit_index++;
            }
            temp_bit.GetComponent<BitMove>().SetPos();
            temp_bit.transform.SetParent(bits);
        }
    }

    public void BitsRemoval()
    {
        for(int i = 0; i < bits.childCount; i++)
        {
            Destroy(bits.GetChild(i).gameObject);
        }
    }
}
