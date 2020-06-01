using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{

    public Button loginButton;
    public SubMenu subMenu;

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
        if (PlayCloudDataManager.Instance.isAuthenticated)
        {
            PlayCloudDataManager.Instance.Login();
        }
        else
        {
            GameObject loginBoard = Instantiate(Resources.Load("Prefabs/TitleScene/LoginMenu", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

            if (loginBoard)
            {
                BGSPlayer.Instance.playBGS("buttonON");
                loginBoard.transform.SetParent(this.transform);
                loginBoard.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                Debug.Log("Cannot load prefab");
            }
        }
    }

    public void ToStageScene()
    {
        if (PlayCloudDataManager.Instance.isAuthenticated || PlayCloudDataManager.Instance.isGuest)
        {
            // SceneManager.LoadScene("GameScene");
            SceneLoader.Instance.LoadScene("GameScene");
        }
    }

    public void OnSettingButtonClicked()
    {
        BGSPlayer.Instance.playBGS("buttonON");
        ShowSubMenu();
    }

    public void ShowSubMenu()
    {
        subMenu.onOpen();
    }

    public void CloseSubMenu()
    {
        subMenu.onClose();
    }
}
