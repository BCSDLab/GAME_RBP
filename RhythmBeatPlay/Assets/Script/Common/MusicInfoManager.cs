using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MusicInfoManager
{
    public static MusicInfo GetMusicInfo(string code)
    {
        return Resources.Load<MusicInfo>($"Prefabs/MusicPrefabs/{code}");
    }
    public static MusicInfo GetMusicInfo(int code)
    {
        return GetMusicInfo(code.ToString("0000"));
    }
    public static MusicInfo GetMusicInfo(int stagenum, int musicnum)
    {
        return GetMusicInfo(stagenum * 100 + musicnum);
    }
}
