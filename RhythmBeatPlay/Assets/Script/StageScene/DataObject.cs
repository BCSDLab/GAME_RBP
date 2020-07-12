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
        DontDestroyOnLoad(this);
        DataObject.inst = this;
    }
}