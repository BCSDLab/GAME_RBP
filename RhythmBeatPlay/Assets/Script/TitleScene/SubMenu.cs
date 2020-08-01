using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLogoutButtonClicked()
    {
        PlayCloudDataManager.Instance.Logout();
        OnCloseButtonClicked();
    }

    public void OnCloseButtonClicked()
    {
        BGSPlayer.Instance.playBGS("buttonOFF");
        onClose();
    }

    public void ShowSettingBoard()
    {
        GameObject settingBoard = Instantiate(Resources.Load("Prefabs/SettingBoardPrefab", typeof(GameObject)), Vector3.zero , Quaternion.identity) as GameObject;
        
        if (settingBoard)
        {
            BGSPlayer.Instance.playBGS("buttonON");
            settingBoard.transform.SetParent(this.transform);
            settingBoard.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Debug.Log("Cannot load prefab");
        }
    }

    public void ShowSettingBoardOnParent(GameObject obj)
    {
        GameObject settingBoard = Instantiate(Resources.Load("Prefabs/SettingBoardPrefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

        if (settingBoard)
        {
            BGSPlayer.Instance.playBGS("buttonON");
            settingBoard.transform.SetParent(obj.transform);
            settingBoard.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Debug.Log("Cannot load prefab");
        }
    }

    public void ShowCreditBoard() { }

    public void ShowCopyrightBoard() { }

    public void onOpen()
    {
        gameObject.SetActive(true);
    }

    public void onClose()
    {
        gameObject.SetActive(false);
    }
}
