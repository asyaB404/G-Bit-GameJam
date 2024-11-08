using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Metronome.timbre
{
    /// <summary>
    /// 音色的抽象类
    /// </summary>
    public abstract class AbsTimbre:ITimbre
    {
        public AbsTimbre(Timbre_SO so)
        {
            _clip = so.AudioClip;
            _name = so.AudioFileName;
        }
        
        
        private AudioClip _clip;
        public AudioClip Clip => _clip;
        
        private EventManager<TimbreEvent> _eventManager = new EventManager<TimbreEvent>();
        public EventManager<TimbreEvent> EventManager => _eventManager;

        private readonly string _name;
        public string Name => _name;
        
    }
}