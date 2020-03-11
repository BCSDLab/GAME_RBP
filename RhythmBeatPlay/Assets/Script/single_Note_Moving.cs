using System.Collections;
using UnityEngine;

public class single_Note_Moving : MonoBehaviour
{

    private float dropTime = 2.0f; // 다음의 시간은 게임매니저로 옮기는게 나을듯.
    public float updateTime = 0.03f;
    public float scale = 0.3f;
    private float minimalSize = 0.3f;
    private float maxSize = 5.0f;

    // 관리 변수
    public float changeScaleFrame = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        SetPosition();
        StartCoroutine("DropNote");
    }
    

    // Update is called once per frame

    private void SetPosition()
    {
        this.transform.localScale = new Vector3(scale, scale, 1);
    }

    private IEnumerator DropNote()
    {
        while (this.transform.localScale.x < maxSize)
        {
            changeScaleFrame = (maxSize - minimalSize) / (dropTime * 1 / updateTime);
            scale += changeScaleFrame;
            this.transform.localScale = new Vector3(scale, scale, 1);
            yield return new WaitForSeconds(updateTime);
        }
    }
}