using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Metronome.timbre
{
    public abstract class AbsTimbre:ITimbre
    {
        public AbsTimbre(AudioClip clip)
        {
            _clip = clip;
            _cells = new();
        }
        
        private AudioClip _clip;
        public AudioClip Clip => _clip;

        private List<MCell> _cells;
        public List<MCell> Cells => _cells;

        //按照ID排序
        public void AddCell()
        {
            _cells.Add(new MCell(new CellInfo(this.GetType(),_cells.Count+1)));
        }

        public void RemoveCell()
        {
            _cells.RemoveAt(_cells.Count - 1);
        }

        //添加自定义音符
        // public void AddCell(MCell cell)
        // {
        //     if (_cells.Contains(cell))
        //     {
        //         return;
        //     }
        //     _cells.Add(cell);
        // }
    }
}