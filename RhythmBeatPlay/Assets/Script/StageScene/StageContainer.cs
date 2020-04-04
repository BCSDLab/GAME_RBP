using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageContainer : MonoBehaviour
{
    public int stageNumber = 0;
    public int stageSize = 10;
    public float spinTime = 0.1f;
    public GameObject preview;
    public Transform[] spawnPoints;
    public Stage stagePrefab;
    public AudioSpectrum audioSpectrum;
    private bool spinning = false;
    private int frontSpawn = 0;
    private List<Stage> stages = new List<Stage>();
    private int stageComparer(Stage s1, Stage s2) //stage 정렬을 위한 비교자
    {
        return s1.number - s2.number;
    }
    private void spawnStage(int relation) //상대 위치로 stage 생성 후 번호순으로 정렬
    {
        int spawnNumber = (frontSpawn + relation + 8) % 8;
        var spawnPosition = spawnPoints[spawnNumber].position;
        var stage = Instantiate(stagePrefab, transform) as Stage;
        stage.init(stageNumber + relation, spawnPosition);
        stages.Add(stage);
        stages.Sort(stageComparer);
    }
    private bool isValidStage(int snum) //stage 위치 유효성 검사
    {
        return snum >= 0 && snum < stageSize;
    }
    void Start()
    {
        Destroy(preview);
        for (int i = -2; i <= 2; ++i)
        {
            if (isValidStage(i + stageNumber))
            {
                spawnStage(i);
            }
        }
        audioSpectrum.play(stages.Find(stage => stage.number == stageNumber).preview);
    }
    private void basisRotate(Quaternion origin, float yAngle)
    {
        transform.rotation = origin;
        transform.Rotate(0, yAngle, 0);
    }
    private IEnumerator stageTurn(int direction) //스테이지를 spinTime동안 회전시킨다. direction 1:left, -1:right
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
        audioSpectrum.play(stages.Find(stage => stage.number==stageNumber).preview);
        spinning = false;
    }
    private void destroyLostStage(int direction) //보이지 않을 스테이지 삭제
    {
        int lostPosition = stageNumber - 2 * direction;
        if (isValidStage(lostPosition))
        {
            var lostStage = stages.Find((s1) => s1.number == lostPosition);
            stages.Remove(lostStage);
            Destroy(lostStage.gameObject);
        }
    }
    public void turn(bool isLeft) //스테이지 회전
    {
        int direction = isLeft ? 1 : -1;
        if (!spinning && isValidStage(stageNumber + direction))
        {
            audioSpectrum.stop();
            destroyLostStage(direction);
            if (isValidStage(stageNumber + 3 * direction))
                spawnStage(3 * direction);
            stageNumber += direction;
            frontSpawn = (frontSpawn + direction + 8) % 8;
            StartCoroutine(stageTurn(direction));
        }
    }
}
