using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSelectManager : SelectManager
{
    public int stageNumber;
    protected override void Start()
    {
        base.Start();
    }
    protected override void objectSelect()
    {
        base.objectSelect();
        DataManager.Instance.musicNumber = base.selectedObjNumber;
        SceneLoader.Instance.LoadScene("GameScene");
    }
    public void backToStageSelect()
    {
        SceneLoader.Instance.LoadScene("StageSelectScene");
    }
}
