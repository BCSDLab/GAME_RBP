using UnityEngine;

public class MusicDataHolder : MonoBehaviour
{
    private static MusicDataHolder instance;

    private MusicData data;

    public static MusicDataHolder Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicDataHolder>();

                if (instance == null)
                {
                    instance = new GameObject("MusicData").AddComponent<MusicDataHolder>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.transform);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ChangeMusicData(MusicData new_data)
    {
        data = new_data;
    }
}