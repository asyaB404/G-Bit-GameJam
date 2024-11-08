using System.Collections.Generic;
using UnityEngine;

namespace Metronome.timbre
{
    //音色接口，继承实现
    public interface ITimbre
    {
        string Name { get; }
        AudioClip Clip { get; }
        
        EventManager<TimbreEvent> EventManager { get; }
    }
}