using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AudioSpectrum : MonoBehaviour
{

    public GameObject barPrefab;
    [Range(0, 180)]
    public float curveAngle = 180f;
    [Min(1)]
    public float distance = 1.5f;
    public int barAmount = 80;
    public int sampleSize = 256;
    public float amplification = 300f;
    public float minYScale = 0.1f;
    public float maxYScale = 50f;
    public float lowIgnore = 0.2f;
    public float highIgnore = 0.2f;
    private float currentVolumeScale = 0.0f;
    public AudioSource baseAudio;
    private GameObject[] barArray;

    float volume_scale
    {
        get
        {
            return DataManager.Instance.music_volume;
        }
        set
        {
            DataManager.Instance.music_volume = value;
        }
    }

    private void instanciateBarArray() //모든 bar 객체 생성
    {
        var betweenAngle = 90 - curveAngle / 2;
        var leftRotation = Quaternion.Euler(0, 0, betweenAngle);
        var rightRotation = Quaternion.Euler(0, 0, -betweenAngle);
        var disX = distance * barAmount / 2 * Mathf.Cos(betweenAngle * Mathf.Deg2Rad);
        var disY = distance * barAmount / 2 * Mathf.Sin(betweenAngle * Mathf.Deg2Rad);
        for (int i = 0; i < barAmount; ++i)
        {
            var position = (float)i * 2 / barAmount - 1;
            bool isLeft = i < barAmount / 2;
            barArray[i] = Instantiate(barPrefab, transform) as GameObject;
            barArray[i].transform.localPosition = new Vector3(disX * position, -disY * Mathf.Abs(position));
            barArray[i].transform.localRotation = isLeft ? leftRotation : rightRotation;
        }
    }
    private void Start()
    {
        barAmount -= barAmount % 2;
        barArray = new GameObject[barAmount];
        instanciateBarArray();
    }
    private void Awake()
    {
        baseAudio.volume = volume_scale;
        DataManager.Instance.music_volume_event.AddListener(changeVolume);
    }
    private void OnDestroy()
    {
        DataManager.Instance.music_volume_event.RemoveListener(changeVolume);
    }
    private void rescaleBar(GameObject bar, float scale) //bar의 yscale 변경
    {
        if (scale > maxYScale)
            scale = maxYScale + (scale - maxYScale) / 10;
        else if (scale < minYScale)
            scale = minYScale;
        var box = bar.transform.GetChild(0);
        box.transform.localScale = new Vector3(1, scale, 1);
        box.transform.localPosition = new Vector3(0.5f, (scale - 1) / 2, 0);
    }
    private float[] reshapeScale(float[] shape) //저음-고음 스케일을 저음-고음-저음 형태로 변경
    {
        var newShape = new float[barAmount];
        int idx = 0;
        for (int i = 0; i < barAmount; i += 2)
            newShape[idx++] = shape[i];
        for (int i = barAmount - 1; i > 0; i -= 2)
            newShape[idx++] = shape[i];
        return newShape;
    }
    private float[] sliceSample(float[] sample, float start, float end) // sample의 중간부분을 추출
    {
        return sample.Skip((int)(sample.Length * start)).Take((int)(sample.Length * (1 - start - end))).ToArray();
    }
    private float[] resizeSample(float[] sample) //sample을 bar의 갯수에 맞게 조정
    {
        int sampleIndex = 0;
        var resized = new float[barAmount];
        for (int i = 0; i < barAmount; ++i)
        {
            int collecting = (int)((float)sample.Length / barAmount * (i + 1));
            for (; sampleIndex < collecting; ++sampleIndex)
                resized[i] += sample[sampleIndex];
        }
        return resized;
    }
    private float[] getSample() //baseAudio의 sample 추출
    {
        var sample = new float[sampleSize];
        baseAudio.GetSpectrumData(sample, 0, FFTWindow.Rectangular);
        return sample;

    }
    private void Update()
    {
        if (baseAudio.isPlaying)
        {
            var sample = getSample();
            var barScale = resizeSample(sliceSample(sample, lowIgnore, highIgnore));
            barScale = reshapeScale(barScale);
            for (int i = 0; i < barAmount; ++i)
            {
                float scale = barScale[i] * amplification;
                rescaleBar(barArray[i], scale);
            }
        }      
    }
    public void play(AudioClip audio)
    {
        gameObject.SetActive(true);
        baseAudio.clip = audio;
        resume();
    }
    public void stop()
    {
        baseAudio.Stop();
        gameObject.SetActive(false);
    }
    public void pause()
    {
        baseAudio.Pause();
    }
    public void changeVolume()
    {
        baseAudio.volume = volume_scale;
    }
    public void resume()
    {
        baseAudio.volume = DataManager.Instance.music_volume;
        baseAudio.Play();
    }
}
