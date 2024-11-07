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
    
    //一个鼓点单元的事件
    private EventManager<CellEventKinds> eventManager = new EventManager<CellEventKinds>();
    public EventManager<CellEventKinds> EventManager => eventManager;

    private bool _canplay;
    public bool Canplay => _canplay;
    
    //cell的信息
    private CellInfo _info;
    public CellInfo Info => _info;
    
    public int ID => _info.Id;

    /// <summary>
    /// 节点播放的音色
    /// </summary>
    /// <param name="timbre">音色</param>
    public void Play(ITimbre timbre)
    {
        if (_canplay)
        {
            Debug.Log("音色 "+timbre + "序号 " + ID);
        }
    }
}
