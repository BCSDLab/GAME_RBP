using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public Button settingButton;
    public GameObject MainMenu;
    public GameObject SubMenu;
    public Text tokenText;
    public Text DriveToken;

    private void Awake()
    {
        // refreshUI(PlayCloudDataManager.Instance.isAuthenticated);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayCloudDataManager.Instance.loginEvent.AddListener(refreshUI);

        getDriveToken();
    }

    // Update is called once per frame
    int tick = 0;
    int temptok = 0;
    int sec = 0;
    void Update()
    {
        tokenText.text = "Token : " + DataManager.Instance.token;
        if (++tick > 60)
        {
            tick = 0;
            sec += 1;
        }
    }

    public void getDriveToken()
    {
        if (!Application.isEditor)
        {
            if (DataManager.Instance.IsConnected)
            {
                PlayCloudDataManager.Instance.LoadFromCloud((string dataToLoad) => { DataManager.Instance.token = int.Parse(dataToLoad); });
                DriveToken.text = "Sec : "+ sec + ", "+ "Drive Token : " + DataManager.Instance.token;
            }
        }
    }

    public void refreshUI(bool isSigned)
    {

        if (isSigned)
        {
            OnLoginUIRefresh(isSigned);
        }
        else
        {
            OnLogOutUIRefresh(isSigned);
        }
    }

    private void OnDestroy()
    {
        PlayCloudDataManager.Instance.loginEvent.RemoveListener(refreshUI);
    }

    private void OnLogOutUIRefresh(bool isSigned)
    {
        MainMenu.SetActive(true);
    }

    private void OnLoginUIRefresh(bool isSigned)
    {
        MainMenu.SetActive(false);
    }

    public void ToStageScene()
    {
        if (PlayCloudDataManager.Instance.isAuthenticated)
        {
            SceneManager.LoadScene("StageScene");
        }
    }

    public void SettingsButtonClicked()
    {
        SubMenu.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    public void SettingsButtonCanceled()
    {
        SubMenu.transform.localPosition = new Vector3(1920f, 0f, 0f);
    }
}
