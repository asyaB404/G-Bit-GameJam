﻿using Cysharp.Threading.Tasks;
using Metronome.timbre;
using UnityEngine;

namespace Metronome
{
    public class MetronomeController
    {
        private MetronomeManager _manager;
        public MetronomeManager Manager => _manager;

        
        /// <summary>
        /// 最大节点数
        /// </summary>
        private int _maxcellnum;
        public int MaxCellNum => _maxcellnum;
        
        
        /// <summary>
        /// 每个音色节点数量
        /// </summary>
        private int _cellCount;
        public int CellCount => _cellCount;
        
        /// <summary>
        /// 音色数量
        /// </summary>
        private int _timbreCount;
        public int TimbreCount => _timbreCount;
        
        private int _timbremaxnum;
        public int TimbreMaxNum => _timbremaxnum;
        
        /// <summary>
        /// 创造节拍控制器
        /// </summary>
        /// <param name="manager">节点管理器</param>
        /// <param name="maxcell">最大节点数</param>
        /// <param name="timbremaxnum">最大音色数</param>
        public MetronomeController(MetronomeManager manager, int maxcell = 1000, int timbremaxnum = 100)
        {
            _manager = manager;
            _maxcellnum = maxcell;
            _timbremaxnum = timbremaxnum;
        }
        
        /// <summary>
        /// 创造节拍控制器
        /// </summary>
        public MetronomeController()
        {
            _manager = new MetronomeManager();
            _maxcellnum = 1000;
        }
        

        /// <summary>
        /// 播放下一格
        /// </summary>
        public void PlayNext()
        {
            foreach (var V in _manager.Metronomemanage)
            {
                var cell =  V.Value.GetNextCell();
                cell.Play(V.Key);
            }
        }
        
        /// <summary>
        /// 添加新的音色与节点链表
        /// </summary>
        /// <param name="timbre"></param>
        public void AddTimbre(ITimbre timbre)
        {
            if (_manager.RegisterTimbre(timbre))
            {
                _timbreCount++;
                for (int i = 0; i < _cellCount; i++)
                {
                    _manager.Metronomemanage[timbre].AddCell();
                }
            }
        }


        /// <summary>
        /// 删除一个音色
        /// </summary>
        /// <param name="timbre">音色</param>
        public void DeleteTimbre(ITimbre timbre)
        {
            if (_manager.DelectTimbre(timbre))
            {
                _timbreCount--;
            }
        }


        /// <summary>
        /// 替换一个音色
        /// </summary>
        /// <param name="beforetimbre">替换的音色</param>
        /// <param name="newtimbre">新音色</param>
        public void ReplaceTimbre(ITimbre beforetimbre,ITimbre newtimbre)
        {
            _manager.RePlaceTimbre(beforetimbre, newtimbre);
        }
        
        
        
        

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="num">添加数量</param>
        public void AddCell(int num)
        {
            if (_maxcellnum < _cellCount + num)
            {
                Debug.LogError("节点数超过最大负荷");
                return;
            }
            for (int i = 0; i < num; i++)
            {
                AddCell();
            }
        }
    
        /// <summary>
        /// 加节点一次添加一个
        /// </summary>
        public void AddCell()
        {
            if (_maxcellnum < _cellCount + 1)
            {
                Debug.LogError("节点数超过最大负荷");
                return;
            }
            foreach (var v in _manager.Metronomemanage)
            {
                v.Value.AddCell();
            }
            _cellCount++;
        }
        
        /// <summary>
        /// 移除节点
        /// </summary>
        public void RemoveCell()
        {
            if (_maxcellnum == 0)
            {
                Debug.LogError("移除节点数太多");
            }
            foreach (var v in _manager.Metronomemanage)
            {
                v.Value.RemoveCell();
            }
            _cellCount--;
        }

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="num">数量</param>
        public void RemoveCell(int num)
        {
            if (_cellCount - num < 0)
            {
                Debug.LogError("移除节点数太多");
                return;
            }
            for (int i = 0; i < num; i++)
            {
                RemoveCell();
            }
        }
    }
    
    
}