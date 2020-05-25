using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note : MonoBehaviour
{
    [SerializeField]
    int bar; // bpm의 마디를 의미.
    [SerializeField]
    int degree; // 각도
    [SerializeField]
    int type; // 종류

    public void setBar(int m_bar)
    {
        bar = m_bar;
    }
    public void setDegree(int m_degree)
    {
        degree = m_degree;
    }
    public void setType(int m_type)
    {
        type = m_type;
    }

    public int getBar()
    {
        return bar;
    }
    public int getDegree()
    {
        return degree;
    }
    public int getType()
    {
        return type;
    }

    // constructor
    public note()
    {
        bar = 0;
        degree = 0;
        type = 0;
    }
    public note(int m_bar, int m_degree, int m_type)
    {
        bar = m_bar;
        degree = m_degree;
        type = m_type;
    }
}
