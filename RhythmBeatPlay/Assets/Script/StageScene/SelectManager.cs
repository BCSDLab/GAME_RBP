using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public Text infomationText;
    public int selectedObjNumber = 0;
    public float spinTime = 0.1f;
    public GameObject preview;
    public Selectable[] objectPrefabs;
    public Transform[] spawnPoints;
    public AudioSpectrum audioSpectrum;
    private bool spinning = false;
    private bool downed = false;
    private int frontSpawn = 0;
    private List<Selectable> objects = new List<Selectable>();
    private Vector3 cursorPosition;
    private void changeFrontObject()
    {
        var frontobj = objects.Find(obj => obj.number == selectedObjNumber);
        infomationText.text = frontobj.objectName;
        audioSpectrum.play(frontobj.preview);
    }
    private int selectableComparer(Selectable s1, Selectable s2) //obj 정렬을 위한 비교자
    {
        return s1.number - s2.number;
    }
    private void spawnObject(int relation) //상대 위치로 obj 생성 후 번호순으로 정렬
    {
        int spawnNumber = (frontSpawn + relation + 8) % 8;
        int objNumber = selectedObjNumber + relation;
        var spawnPosition = spawnPoints[spawnNumber].position;
        var obj = Instantiate(objectPrefabs[objNumber], transform) as Selectable;
        obj.init(objNumber, spawnPosition);
        objects.Add(obj);
        objects.Sort(selectableComparer);
    }
    private bool isValidObject(int snum) //obj 위치 유효성 검사
    {
        return snum >= 0 && snum < objectPrefabs.Length;
    }
    protected virtual void Start()
    {
        Destroy(preview);
        for (int i = -2; i <= 2; ++i)
        {
            if (isValidObject(i + selectedObjNumber))
            {
                spawnObject(i);
            }
        }
        changeFrontObject();
    }
    private void checkObjSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cursorPosition = Input.mousePosition;
            downed = true;
        }
        else if (downed)
        {
            if ((cursorPosition - Input.mousePosition).magnitude > 100f)
            {
                downed = false;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                var ray = Camera.main.ScreenPointToRay(cursorPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Selectable" && hit.collider.GetComponent<Selectable>().number == selectedObjNumber)
                    {
                        objectSelect();
                    }
                }
            }
        }
    }
    protected virtual void Update()
    {
        checkObjSelect();
    }
    private void basisRotate(Quaternion origin, float yAngle)
    {
        transform.rotation = origin;
        transform.Rotate(0, yAngle, 0);
    }
    private IEnumerator objectTurn(int direction) //스테이지를 spinTime동안 회전시킨다. direction 1:left, -1:right
    {
        float v = 90f / spinTime;
        float a = -v / spinTime;
        float t = 0f;
        var originRotation = transform.rotation;
        spinning = true;
        while (t < spinTime)
        {
            t += Time.deltaTime;
            float nextAngle = (a * t * t / 2 + v * t) * direction;
            basisRotate(originRotation, nextAngle);
            yield return null;
        }
        basisRotate(originRotation, 45f * direction);
        changeFrontObject();
        spinning = false;
    }
    private void destroyLostObject(int direction) //보이지 않을 스테이지 삭제
    {
        int lostPosition = selectedObjNumber - 2 * direction;
        if (isValidObject(lostPosition))
        {
            var lostobj = objects.Find((s1) => s1.number == lostPosition);
            objects.Remove(lostobj);
            Destroy(lostobj.gameObject);
        }
    }
    public void turn(bool isLeft) //스테이지 회전
    {
        int direction = isLeft ? 1 : -1;
        if (!spinning && isValidObject(selectedObjNumber + direction))
        {
            audioSpectrum.stop();
            destroyLostObject(direction);
            if (isValidObject(selectedObjNumber + 3 * direction))
                spawnObject(3 * direction);
            selectedObjNumber += direction;
            frontSpawn = (frontSpawn + direction + 8) % 8;
            StartCoroutine(objectTurn(direction));
        }
    }
    protected virtual void objectSelect()
    {

    }
}
