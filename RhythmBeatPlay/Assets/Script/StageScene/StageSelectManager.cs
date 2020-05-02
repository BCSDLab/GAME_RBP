using UnityEngine;
using UnityEngine.SceneManagement;
public class StageSelectManager : SelectManager
{
    protected override void Start()
    {
        base.selectedObjNumber = DataObject.inst.stageNumber;
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void objectSelect()
    {
        DataObject.inst.stageNumber = base.selectedObjNumber;
        SceneManager.LoadScene("MusicScene");
    }
}
