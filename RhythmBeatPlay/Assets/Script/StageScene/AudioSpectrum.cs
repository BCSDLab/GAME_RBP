using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    public AudioSource baseAudio;
    private GameObject[] barArray;
    // Start is called before the first frame update
    void Start()
    {
        barAmount = barAmount - barAmount % 2;
        barArray = new GameObject[barAmount];
        var betweenAngle = 90 - curveAngle / 2;
        var leftRotation = Quaternion.Euler(0, 0, betweenAngle);
        var rightRotation = Quaternion.Euler(0, 0, -betweenAngle);
        var disX = distance * barAmount / 2 * Mathf.Cos(betweenAngle * Mathf.Deg2Rad);
        var disY = distance * barAmount / 2 * Mathf.Sin(betweenAngle * Mathf.Deg2Rad);
        var leftBottom = new Vector3(-disX, -disY);
        var rightBottom = new Vector3(disX, -disY);
        for (int i = 0; i < barAmount / 2; ++i)
        {
            var barInst = Instantiate(barPrefab, transform) as GameObject;
            barArray[i] = barInst;
            var position = (float)(barAmount / 2 - i) / (barAmount / 2);
            barInst.transform.localPosition = leftBottom * position;
            barInst.transform.localRotation = leftRotation;
        }
        for (int i = barAmount / 2; i < barAmount; ++i)
        {
            var barInst = Instantiate(barPrefab, transform) as GameObject;
            barArray[i] = barInst;
            var position = (float)i / (barAmount / 2) -1;
            barInst.transform.localPosition = rightBottom * position;
            barInst.transform.localRotation = rightRotation;

        }
    }
    float[] getBarScales()
    {
        var sample = new float[sampleSize];
        baseAudio.GetSpectrumData(sample, 0, FFTWindow.Rectangular);
        int sampleIndex = 1;
        var barscales = new float[barAmount];
        for (int i = 0; i < barAmount; ++i)
        {
            int collecting = (int)((float)sample.Length / barAmount * (i + 1));
            for (; sampleIndex < collecting; ++sampleIndex)
                barscales[i] += sample[sampleIndex];
        }
        return barscales;
    }
    void barAmplify(GameObject bar, float scale)
    {
        if (scale > maxYScale)
            scale = maxYScale + (scale - maxYScale) / 10;
        var box = bar.transform.GetChild(0);
        box.transform.localScale = new Vector3(1, scale, 1);
        box.transform.localPosition = new Vector3(0.5f, (scale - 1) / 2, 0);
    }
    // Update is called once per frame
    void Update()
    {
        var barScale = getBarScales();
        for (int i = 0; i < barAmount / 2; ++i)
        {
            float scale = barScale[i] * amplification + minYScale;
            barAmplify(barArray[i], scale);
        }
        for (int i = barAmount / 2; i < barAmount; ++i)
        {
            float scale = barScale[i] * amplification + minYScale;
            barAmplify(barArray[i], scale);
        }
    }
}
