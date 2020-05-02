using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    protected static SceneLoader instance;
    //Singleton
    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SceneLoader>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    [SerializeField]
    private float fadeTime = 1.0f;
    [SerializeField]
    private CanvasGroup sceneLoaderCanvasGroup;
    [SerializeField]
    private Image progressBar;
    private string loadSceneName;
    //로더 객체 생성
    public static SceneLoader Create()
    {
        var SceneLoaderPrefab = Resources.Load<SceneLoader>("Prefabs/SceneLoader");
        return Instantiate(SceneLoaderPrefab);
    }
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    //씬 로드
    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += LoadSceneEnd;
        loadSceneName = sceneName;
        StartCoroutine(Load(sceneName));
    }
    //로딩화면을 fade in 하고 프로그레스바 동기화
    private IEnumerator Load(string sceneName)
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
    //씬 로드가 종료되면 fade out
    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }
    //fade in/out
    private IEnumerator Fade(bool isFadeIn)
    {
        int alphaBegin = isFadeIn ? 0 : 1;
        float timer = 0f;
        while (timer <= fadeTime)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            sceneLoaderCanvasGroup.alpha = Mathf.Lerp(alphaBegin, 1 - alphaBegin, timer/fadeTime);
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}