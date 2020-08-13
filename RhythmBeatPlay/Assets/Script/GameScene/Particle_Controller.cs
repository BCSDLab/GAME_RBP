using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Controller : MonoBehaviour
{
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this);
    } 
}
