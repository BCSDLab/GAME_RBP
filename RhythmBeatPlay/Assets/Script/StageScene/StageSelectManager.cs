using UnityEngine.SceneManagement;
public class StageSelectManager : SelectManager
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void objectSelect()
    {
        SceneManager.LoadScene("MusicScene");
    }
}
