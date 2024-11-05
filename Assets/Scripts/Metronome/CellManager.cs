using System;
using System.Collections.Generic;
using Metronome.timbre;
using UnityEngine;

namespace Metronome
{
    public class CellManager
    {
        private Dictionary<Type, ITimbre> _timbres = new Dictionary<Type, ITimbre>();
        public IReadOnlyDictionary<Type,ITimbre> Timbres => _timbres;

        private int _numcell ;
        public int NumCell => _numcell;

        //管理器添加音色
        public void AddTimbre(ITimbre timbre)
        {
            if (_timbres.ContainsKey(timbre.GetType()))
            {
                Debug.LogWarning("Timbre already added");
                return;
            }
            _timbres.Add(timbre.GetType(), timbre);
            for (int i = 0; i < _numcell; i++)
            {
                _timbres[timbre.GetType()].AddCell();
            }
        }
        
        //管理器删除音色
        public void PopTimbre(ITimbre timbre)
        {
            if (_timbres.ContainsKey(timbre.GetType()))
            {
                _timbres.Remove(timbre.GetType());
                return;
            }
            Debug.LogWarning("No timbre found");
        }



        /// <summary>
        /// 每个音色的格子数量是否一致
        /// </summary>
        /// <returns></returns>
        public bool CheckCells()
        {
            int data = -1;
            foreach (var t in _timbres)
            {
                if (data != -1 && data != t.Value.Cells.Count)
                {
                    return false;
                }
                data = t.Value.Cells.Count;
            }
            return true;
        }
        
        
        /// <summary>
        /// 为每一个音色添加单元数
        /// </summary>
        /// <param name="nums">数量</param>
        public void AddCell(int nums)
        {
            _numcell += nums;
            foreach (var t in _timbres)
            {
                for (var i = 0; i < nums; i++)
                {
                    t.Value.AddCell();
                }
            }
        }
        
        public void RemoveCell(int nums)
        {
            foreach (var t in _timbres)
            {
                for (var i = 0; i < _numcell; i++)
                {
                    if (i > _numcell  - nums-1)
                    {
                        t.Value.RemoveCell();
                    }
                }
            }
            _numcell -= nums;
        }
    }
}