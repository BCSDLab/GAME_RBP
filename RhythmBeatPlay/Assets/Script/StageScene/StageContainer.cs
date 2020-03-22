using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageContainer : MonoBehaviour
{
    public float spinTime = 0.1f;
    public GameObject StagePrefab;
    private bool spinning = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Turn(bool isLeft)
    {
        if (!spinning)
        {
            if (isLeft)
                StartCoroutine(stageTurn(-1));
            else
                StartCoroutine(stageTurn(1));
        }
    }
    public IEnumerator stageTurn(int direction)
    {
        float v = 90f / spinTime;
        float a = -v / spinTime;
        float t = 0f;
        var currRotation = transform.rotation;
        spinning = true;
        while (t < spinTime)
        {
            t += Time.deltaTime;
            transform.rotation = currRotation;
            float nextAngle = (a * t * t / 2 + v * t) * direction;
            transform.Rotate(0, nextAngle, 0);
            yield return null;
        }
        transform.rotation = currRotation;
        transform.Rotate(0, 45f*direction, 0);
        spinning = false;
    }
}
