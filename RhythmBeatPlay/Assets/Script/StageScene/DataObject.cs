using UnityEngine;

public class DataObject : MonoBehaviour
{
    public static DataObject inst;
    public int stageNumber = 0;

    private void Awake()
    {
        if (DataObject.inst != null)
        {
            Destroy(gameObject);
            return;
        }
        DataObject.inst = this;
        DontDestroyOnLoad(this);
    }
}