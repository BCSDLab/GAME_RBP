using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataHolder : MonoBehaviour
{
    private static MusicDataHolder instance;

    MusicData data;

    public static MusicDataHolder Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicDataHolder>();

                if (instance == null)
                {
                    instance = new GameObject("MusicData").AddComponent<MusicDataHolder>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusicData(MusicData new_data)
    {
        data = new_data;
    }

}
