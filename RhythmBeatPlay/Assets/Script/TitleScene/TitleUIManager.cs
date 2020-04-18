using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    private static TitleUIManager instance;

    public Button loginButton;
    public GameObject subMenu;

    public static TitleUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TitleUIManager>();

                return instance;
            }
            return instance;
        }
    }

    #region Unity Function

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        PlayCloudDataManager.Instance.loginEvent.AddListener(LoginRefreshUI);
    }

    private void OnDestroy()
    {
        PlayCloudDataManager.Instance.loginEvent.RemoveListener(LoginRefreshUI);
    }
    #endregion

    void Update()
    {
        
    }

    public void LoginRefreshUI(bool isSigned)
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

    private void OnLogOutUIRefresh(bool isSigned)
    {
        loginButton.gameObject.SetActive(true);
    }

    private void OnLoginUIRefresh(bool isSigned)
    {
        loginButton.gameObject.SetActive(false);
    }

    public void OnLoginButtonClicked()
    {
        PlayCloudDataManager.Instance.Login();
    }

    public void ToStageScene()
    {
        if (PlayCloudDataManager.Instance.isAuthenticated)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void OnSettingButtonClicked()
    {
        BGSPlayer.Instance.playBGS("buttonON");
        ShowSubMenu();
    }

    public void ShowSubMenu()
    {
        subMenu.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    public void CloseSubMenu()
    {
        subMenu.transform.localPosition = new Vector3(1920f, 0f, 0f);
    }
}
