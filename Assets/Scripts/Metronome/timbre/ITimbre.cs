using System.Collections.Generic;
using UnityEngine;

namespace Metronome.timbre
{
    //音色接口，继承实现
    public interface ITimbre
    {
        AudioClip Clip { get; }
        List<MCell> Cells { get; }
        public void AddCell();
        
        public void RemoveCell();
    }
}