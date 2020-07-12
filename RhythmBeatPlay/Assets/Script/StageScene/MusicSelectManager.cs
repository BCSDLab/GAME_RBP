using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSelectManager : SelectManager
{
    public int stageNumber;
    protected override void Start()
    {
        base.Start();
        stageNumber = DataObject.inst.stageNumber;
    }
    protected override void objectSelect()
    {
        base.objectSelect();
        SceneLoader.Instance.LoadScene("GameScene");
    }
    public void backToStageSelect()
    {
        SceneLoader.Instance.LoadScene("StageScene");
    }
}
