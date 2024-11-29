using System;
using Metronome;
using Metronome.timbre;
using UnityEngine;

public class MCell
{
    public MCell(CellInfo info)
    {
        _info = info;
    }
    

    /// <summary>
    /// 尝试一下
    /// </summary>
    private bool _canplay;
    public bool Canplay => _canplay;
    
    //cell的信息
    private CellInfo _info;
    public CellInfo Info => _info;
    
    public int ID => _info.Id;


    /// <summary>
    /// 改变单元的播放状态
    /// </summary>
    public void ChangePlayState()
    {
        _canplay = !_canplay;
    }

    /// <summary>
    /// 节点播放的音色
    /// </summary>
    /// <param name="timbre">音色</param>
    public void Play(ITimbre timbre)
    {
        if (_canplay)
        {
            timbre.EventManager.Dispatch(TimbreEvent.BegainHit);
            AudioManager.Instance.PlaySound(timbre.Clip);
//            Debug.Log("音色 "+timbre + "序号 " + ID);
            timbre.EventManager.Dispatch(TimbreEvent.AfterHit);
        }
    }
}
