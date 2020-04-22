using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPMcheck : MonoBehaviour
{
    // 시간 보정 값.
    public float time_Fixed;

    // 백그라운드 노래
    public AudioSource bgMusic;

    // 메트로놈 요소
    public double bpm;
    public double startTick;
    public double nextTick = 0.0f;
    public double sampleRate = 0.0f;

    // second per beat.bpm의 반대.
    private double spb;
    private bool ticked = false;

    // 비트카운트
    private int bitCount = 0;

    // 음악 켜짐 관련
    public Button btn_Start;
    private bool musicOnOff;
    private Coroutine r1;
    private Coroutine r2;

    void Start()
    {
        Screen.SetResolution(Screen.width, Screen.height, true);
        this.startTick = AudioSettings.dspTime + time_Fixed;
        //시작틱은, AudioSettings.dspTime; 
        // 오디오 시스템의 현재 시각을 돌려줍니다.
        this.sampleRate = AudioSettings.outputSampleRate;
        //샘플레이트에 믹서의 현재 출력 비율을 가져옵니다.
        this.nextTick = startTick + (60d / bpm);
        //다음틱은 오디오시스템의 현재 시각 + 60/bpm startTick + (spb)

    }

    // 음악 시작 / 일시정지
    public void MusicStartPause()
    {
        if (!this.musicOnOff)
        {
            //음악 시작
            Debug.Log("Song On");
            this.bgMusic.gameObject.GetComponent<AudioSource>().Play();
            this.r1 = StartCoroutine(this.Metronome());
            //this.r2 = StartCoroutine(this.Up());
            this.musicOnOff = true;
        }
        else
        {
            this.bgMusic.gameObject.GetComponent<AudioSource>().Stop();
            StopCoroutine(r1);
            //StopCoroutine(r2);
            this.musicOnOff = false;
            this.bitCount = 0;
        }
    }

    //메트로놈 설정
    private IEnumerator Metronome()
    {
        while (true)
        {
            this.spb = 60.0f / bpm;
            double dspTime = AudioSettings.dspTime;
            //오디오 시스템의 현재 시각

            //오디오 시스템의 현재시각이 Start에서 구한 nextTick보다 크거나 같으면
            while (dspTime >= nextTick)
            {
                ticked = false;
                //bool 틱 = false;
                nextTick += this.spb;
                if (!ticked && nextTick >= AudioSettings.dspTime && this.bgMusic.isPlaying)
                {
                    //bool ticked 를 트루로
                    ticked = true;
                    //매 틱마다 호출되는 bpm 체크 및 노트 스포닝 구문.
                    this.OnTick();
                }
            }
            yield return null;
        }
    }

    // 디버그용 비트 체크기.
    void OnTick()
    {
        // 노트 스폰.
        GameObject.Find("note_spawner").GetComponent<note_spawning>().noteSpawn(this.bitCount);
        /* bpm check용 구문.
        if (this.bitCount % 2 == 0)
        {
            Debug.LogFormat("<color=red>{0}</color>", this.bitCount);
        }
        else
        {
            Debug.LogFormat("<color=blue>{0}</color>", this.bitCount);
        }*/
        this.bitCount++;
    }
}
