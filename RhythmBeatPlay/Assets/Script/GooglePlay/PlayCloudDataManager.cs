// code reference: http://answers.unity3d.com/questions/894995/how-to-saveload-with-google-play-services.html
//gpg
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Collections;

//for encoding
using System.Text;
using UnityEngine;

//for extra save ui
//for text, remove
using UnityEngine.Events;

public class LoginEvent : UnityEvent<bool>
{
}

public class PlayCloudDataManager : MonoBehaviour
{
    private static PlayCloudDataManager instance;
    public LoginEvent loginEvent;

    public static PlayCloudDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayCloudDataManager>();

                if (instance == null)
                {
                    instance = new GameObject("GoogleAuth").AddComponent<PlayCloudDataManager>();
                }
            }

            return instance;
        }
    }

    public bool isGuest
    {
        get;
        private set;
    }

    public bool isProcessing
    {
        get;
        private set;
    }

    public string loadedData
    {
        get;
        private set;
    }

    private string m_saveFileName
    {
        get
        {
            if (isAuthenticated)
            {
                return localUserId + "_Save_data";
            }
            return "Local_Save_Data";
        }
    }

    public bool isAuthenticated
    {
        get
        {
            if (Application.isEditor)
            {
                return false;
            }
            else
            {
                return Social.localUser.authenticated;
            }
        }
    }

    public string localUserId
    {
        get
        {
            if (isAuthenticated)
            {
                return Social.localUser.id;
            }
            return "LOCAL_USER";
        }
        private set { }
    }

    private void InitiatePlayGames()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        Debug.Log("PlayGamesPlatform Initialized");
    }

    private void Awake()
    {
        InitiatePlayGames();

        //initiate event
        if (loginEvent == null)
            loginEvent = new LoginEvent();

        DontDestroyOnLoad(transform.gameObject);
    }

    public void GuestLogin()
    {
        isGuest = true;
        loginEvent.Invoke(isGuest);
    }

    public void Login()
    {
        PlayGamesPlatform.Activate();
        if (!Application.isEditor)
        {
            // Try logging in if it is not running in the editor

            if (!Social.localUser.authenticated)
            {
                Social.localUser.Authenticate((bool success) =>
                {
                    if (!success)
                    {
                        Debug.Log("Fail Login");
                    }
                    else
                    {
                        Debug.Log("Login Succeed");
                    }
                });
            }
            loginEvent.Invoke(isAuthenticated);
        }
        else
        {
            loginEvent.Invoke(true);
        }
    }

    public void Logout()
    {
         PlayGamesPlatform.Instance.SignOut();
        isGuest = false;
        if (!Application.isEditor)
        {
            loginEvent.Invoke(isAuthenticated);
        }
        else
        {
            loginEvent.Invoke(false);
        }
    }

    private void ProcessCloudData(byte[] cloudData)
    {
        if (cloudData == null)
        {
            Debug.Log("No Data saved to the cloud");
            return;
        }

        string progress = BytesToString(cloudData);
        loadedData = progress;
    }

    /*
     * How to use
     * PlayCloudDataManager.Instance.LoadFromCloud( (string dataToLoad ) = > { [player_data_manager_name].Instance.[data_name] = long.parse(DataToLoad); } );
     * */

    public void LoadFromCloud(Action<string> afterLoadAction)
    {
        if (isAuthenticated && !isProcessing)
        {
            StartCoroutine(LoadFromCloudRoutin(afterLoadAction));
        }
        else
        {
            Login();
        }
    }

    private IEnumerator LoadFromCloudRoutin(Action<string> loadAction)
    {
        isProcessing = true;
        Debug.Log("Loading game progress from the cloud.");

        ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
            m_saveFileName, //name of file.
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime,
            OnFileOpenToLoad);

        while (isProcessing)
        {
            yield return null;
        }

        loadAction.Invoke(loadedData);
    }

    /*
     * How to use
     * PlayerCloudDataManager.Instance.SaveToCloud( [player_data_manager_name].Instance.[data_name].ToString());
     *  */

    public void SaveToCloud(string dataToSave)
    {
        if (isAuthenticated)
        {
            loadedData = dataToSave;
            isProcessing = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(m_saveFileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnFileOpenToSave);
        }
        else
        {
            Login();
        }
    }

    private void OnFileOpenToSave(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            byte[] data = StringToBytes(loadedData);

            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

            SavedGameMetadataUpdate updatedMetadata = builder.Build();

            ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(metaData, updatedMetadata, data, OnGameSave);
        }
        else
        {
            Debug.LogWarning("Error opening Saved Game" + status);
        }
    }

    private void OnFileOpenToLoad(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(metaData, OnGameLoad);
        }
        else
        {
            Debug.LogWarning("Error opening Saved Game" + status);
        }
    }

    private void OnGameLoad(SavedGameRequestStatus status, byte[] bytes)
    {
        if (status != SavedGameRequestStatus.Success)
        {
            Debug.LogWarning("Error Saving" + status);
        }
        else
        {
            ProcessCloudData(bytes);
        }

        isProcessing = false;
    }

    private void OnGameSave(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        if (status != SavedGameRequestStatus.Success)
        {
            Debug.LogWarning("Error Saving" + status);
        }

        isProcessing = false;
    }

    private byte[] StringToBytes(string stringToConvert)
    {
        return Encoding.UTF8.GetBytes(stringToConvert);
    }

    private string BytesToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }
}