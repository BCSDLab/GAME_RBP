using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInfo : MonoBehaviour
{
    [SerializeField]
    AudioClip music;

    [SerializeField]
    string title;

    [SerializeField]
    string artist;

    [SerializeField]
    string album;

    [SerializeField]
    int noteCount;

    [SerializeField]
    int bpm;

    [SerializeField]
    float musicLengthSecond;

    // must be scaled 1:1
    [SerializeField]
    Sprite titleSprite;

    // must be scaled 16:9
    [SerializeField]
    Sprite backgroundSprite;
}
