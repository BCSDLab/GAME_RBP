using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fx_SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(this,0.3f);
    }
}
