using UnityEngine;
using UnityEngine.SceneManagement;
public class StageSelectManager : SelectManager
{
    protected override void Start()
    {
        base.selectedObjNumber = DataManager.Instance.stageNumber;
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void objectSelect()
    {
        base.objectSelect();
        DataManager.Instance.stageNumber = base.selectedObjNumber;
        SceneLoader.Instance.LoadScene("MusicSelectScene");
    }
}
