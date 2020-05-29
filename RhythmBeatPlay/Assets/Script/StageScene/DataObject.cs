using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObject : MonoBehaviour
{
    public static DataObject inst;
    public int stageNumber = 0;
    void Awake()
    {
        if (DataObject.inst != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        DataObject.inst = this;
    }

}
