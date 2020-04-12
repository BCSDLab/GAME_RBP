using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSPlayer : MonoBehaviour
{
    [SerializeField]
    AudioSource bgsPlayer;

    Dictionary<string, AudioClip> bgs_set;

    float volume_scale
    {
        get
        {
            return DataManager.Instance.bgs_volume;
        }
        set
        {
            DataManager.Instance.bgs_volume = value;
        }
    }

    private void Awake()
    {
        bgsPlayer.volume = 1.0f;
        bgsPlayer.loop = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Input the name with ".mp3"
    public void playBGS(string name)
    {
        if (bgs_set.ContainsKey(name))
        {
            bgsPlayer.PlayOneShot(bgs_set[name], volume_scale);
        }
        else
        {
            AudioClip bgsClip = Resources.Load("Asset/BGS/" + name) as AudioClip;
            if (bgsClip)
            {
                bgs_set.Add(name, bgsClip);
                bgsPlayer.PlayOneShot(bgsClip);
                return;
            }

            // send Debug error message here

            Debug.Log("Cannot find BGS source from project or game");
        }
    }

    public void preloadBGS(string name)
    {
        AudioClip bgsClip = Resources.Load("Asset/BGS/" + name) as AudioClip;
        if (bgsClip)
        {
            bgs_set.Add(name, bgsClip);
        }
    }

    public void loadAllBGS()
    {
        foreach (string filename in System.IO.Directory.GetFiles("Asset/BGS"))
        {
            AudioClip bgsClip = Resources.Load("Asset/BGS/" + filename) as AudioClip;
            if (bgsClip)
            {
                bgs_set.Add(filename, bgsClip);
            }
        }
    }
}
