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
    public void backToStageSelect()
    {
        SceneManager.LoadScene("StageScene");
    }
}
