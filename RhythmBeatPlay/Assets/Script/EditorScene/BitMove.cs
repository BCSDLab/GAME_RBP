using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitMove : MonoBehaviour
{
    public int bitcnt;
    public int num_note;
    public List<note> note_data = new List<note>();
    double speed;
    private void Start()
    {
        speed = EditorDataParser.instance.rbpm / 6 * 4;
    }

    public void SetPos() // 각 노트 배치
    {
        for (int i = 0; i < note_data.Count; i++)
        {
            GameObject notebit = this.transform.GetChild(i).gameObject;
            notebit.SetActive(true);
            switch (note_data[i].getType())
            {
                case 0:
                    notebit.GetComponent<SpriteRenderer>().color = new Color(1.0f,0f,0f);
                    break;
                case 1:
                    notebit.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.4f, 1.0f);
                    break;
                case 2:
                    notebit.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.0f, 1.0f);
                    break;
            }
            notebit.transform.localPosition = new Vector3(GetXPos(note_data[i].getDegree()), 0, 0);
        }
    }

    private float GetXPos(int degree) // 전체 transform 내의 노트 위치 확보하는 구문
    {
        float deg2decimal = (degree / 180f * 8.375f) - 0.225f;
        return deg2decimal;
    }

    public void AddNote(note data) // 주어진 데이터에 따라 노트 추가하는 구문.
    {
        note_data.Add(data);
        //print(note_data[0].getBar());
        num_note++;
    }
    private void DeleteSelf()
    {
        Destroy(this.gameObject);
    }

    // 배 bit 마다 40씩 움직여야 함.
    // fixedupdate는 초당 50번 업데이트됨.
    private void FixedUpdate()
    {
        // 매 비트마다 40씩 움직이려면, 매 초당 비트수 x 40만큼만 움직이믄되네
        if (!EditorManager.instance.is_pause)
        {
            this.transform.position += new Vector3(0, (float)(-speed * Time.deltaTime), 0);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("되나?");
        EditorManager.instance.ChangeEditBit(this.bitcnt);
        this.transform.GetChild(3).gameObject.SetActive(true);
    }

    

}
