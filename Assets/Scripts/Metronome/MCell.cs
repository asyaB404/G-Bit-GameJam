using System;
using Metronome;

public class MCell
{
    public MCell(CellInfo info)
    {
        _info = info;
    }
    
    //一个鼓点单元的事件
    private EventManager<CellEventKinds> eventManager = new EventManager<CellEventKinds>();
    public EventManager<CellEventKinds> EventManager => eventManager;
    
    //cell的信息
    private CellInfo _info;
    public CellInfo Info => _info;
    
}
