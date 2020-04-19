using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSelectManager : SelectManager
{
    public int stageNumber;
    protected override void Start()
    {
        base.Start();
        var stage = GameObject.Find("StageSelectManager").GetComponent<StageSelectManager>();
        stageNumber = stage.selectedObjNumber;
        Destroy(stage.gameObject);
    }
}
