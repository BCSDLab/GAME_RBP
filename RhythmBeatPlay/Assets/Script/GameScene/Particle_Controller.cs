using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Controller : MonoBehaviour
{
    void Awake()
    {
        GameObject clone = this.gameObject;
        Destroy(clone, 0.5f);
    }
}
