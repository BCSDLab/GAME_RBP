using System.Collections;
using UnityEngine;

public class single_Note_Moving : MonoBehaviour
{
    private Vector3 minimalSize = new Vector3(0, 0, 1);
    private Vector3 maxSize = new Vector3(5, 5, 1);
    // 관리 변수
    public float changeScaleFrame = 0.0f;

    public float timeStartedLerp;
    // Start is called before the first frame update
    private void Start()
    {
        this.transform.localScale = new Vector3(0, 0, 1);
        timeStartedLerp = Time.time;
    }

    private void Update()
    {
        Lerp();
    }

    public Vector3 Lerp(float lerpTime = 2)
    {
        float timeSinceStarted = Time.time - timeStartedLerp;
        float percentageComplete = timeSinceStarted / lerpTime;

        var result = Vector3.Lerp(minimalSize, maxSize, percentageComplete);
        this.transform.localScale = result;
        return result;
    }
}