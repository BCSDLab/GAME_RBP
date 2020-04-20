using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class DataManager : MonoBehaviour
{

    private static DataManager instance;

    #region public member

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();
                if (instance == null)
                {
                    instance = new GameObject("PlayerData").AddComponent<DataManager>();
                }
            }
            return instance;
        }
    }

    // 인터넷에 연결되어 있는지 검사하는 변수
    public bool IsConnected
    {
        get
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return false;
            }
            else return true;
        }
        private set
        {
            if (Application.isEditor)
            {
                IsConnected = value;
            }
        }
    }

    // 곡 또는 스테이지를 해금하는 데에 필요한 토큰
    public int token
    {
        get
        {
            if (!Application.isEditor)
            {
                if (IsConnected)
                {
                    PlayCloudDataManager.Instance.LoadFromCloud((string dataToLoad) => { DataManager.Instance.token = int.Parse(dataToLoad); });
                    return token;
                }
            }
            return PlayerPrefs.GetInt( PlayCloudDataManager.Instance.localUserId + "_token", 0);
        }
        set
        {
            PlayerPrefs.SetInt( PlayCloudDataManager.Instance.localUserId + "_token", value);
            if (!Application.isEditor)
            {
                if (IsConnected)
                {
                    PlayCloudDataManager.Instance.SaveToCloud(DataManager.Instance.token.ToString());
                }
            }
            Debug.Log("Token : " + token);
        }
    }

    public float music_volume
    {
        get
        {
            return PlayerPrefs.GetFloat("music_volume", 0.5f);
        }
        set
        {
            PlayerPrefs.SetFloat("music_volume", value);
        }
    }

    public float bgs_volume
    {
        get
        {
            return PlayerPrefs.GetFloat("bgs_volume", 0.5f);
        }
        set
        {
            PlayerPrefs.SetFloat("bgs_volume", value);
        }
    }

    #endregion

    #region public function

    public void increaseToken()
    {
        token += 1;
    }

    public void decreaseToken()
    {
        if (token > 0)
        {
            token -= 1;
        }
    }

    public void LoadData()
    {
        if (!Application.isEditor)
        {
            if (IsConnected)
            {
                // token = token;
            }
        }
    }

    public void SaveData()
    {
        if (!Application.isEditor)
        {
            if (IsConnected)
            {
                PlayCloudDataManager.Instance.SaveToCloud(DataManager.Instance.token.ToString());
            }
        }
    }

    public void UpdateData()
    {
        if (!Application.isEditor)
        {
            if (IsConnected)
            {

            }
        }
    }

    #endregion

    #region unity function

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    #endregion

    #region private function

    private bool IsRuntime() {
        if (!Application.isEditor)
        {
            if (IsConnected)
            {
                return true;
            }
        }
        return false;
    }

    #endregion
}
