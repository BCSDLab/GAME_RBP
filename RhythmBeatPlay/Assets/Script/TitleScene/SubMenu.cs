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

    public void onLogoutButtonClicked()
    {
        PlayCloudDataManager.Instance.Logout();
        onCloseButtonClicked();
    }

    public void onCloseButtonClicked()
    {
        TitleUIManager.Instance.CloseSubMenu();
    }

    public void ShowSettingBoard() { }

    public void ShowCreditBoard() { }

    public void ShowCopyrightBoard() { }

}
