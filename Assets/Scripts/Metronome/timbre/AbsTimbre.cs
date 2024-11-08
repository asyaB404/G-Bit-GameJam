using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Metronome.timbre
{
    public abstract class AbsTimbre:ITimbre
    {
        public AbsTimbre(AudioClip clip,string name)
        {
            _clip = clip;
            _name = name;
        }
        
        
        private AudioClip _clip;
        public AudioClip Clip => _clip;

        private readonly string _name;
        public string Name => _name;
        
        
    }
}