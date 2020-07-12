using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingMovement : MonoBehaviour
{
    public GameObject leftWing;
    public GameObject rightWing;
    public float minDistance = 4.2f;
    public float maxDistance = 10.0f;
    public float speed = 1f;
    public float waitAtSide = 0.1f;

    private void Start()
    {
        StartCoroutine(MoveWing());
    }

    IEnumerator MoveWing()
    {
        float dist = minDistance;
        float direct = 1f;
        while (true)
        {
            while (minDistance <= dist && dist <= maxDistance)
            {
                leftWing.transform.localPosition = new Vector3(-dist, 0, 0);
                rightWing.transform.localPosition = new Vector3(dist, 0, 0);
                dist += speed * Time.deltaTime * direct;
                yield return null;
            }
            if (dist > maxDistance)
                dist = maxDistance;
            else
                dist = minDistance;
            direct *= -1;
            yield return new WaitForSeconds(waitAtSide);
        }
    }
}
