using Cysharp.Threading.Tasks;
using Metronome.timbre;
using UnityEngine;

namespace Metronome
{
    public class MetronomeModelController:IMetronom
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


        private int _newcell;
        public int NewCell => _newcell;
        
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
        public MetronomeModelController(MetronomeManager manager, int maxcell = 1000, int timbremaxnum = 100)
        {
            _manager = manager;
            _maxcellnum = maxcell;
            _timbremaxnum = timbremaxnum;
        }
        
        /// <summary>
        /// 创造节拍控制器
        /// </summary>
        public MetronomeModelController()
        {
            _manager = new MetronomeManager();
            _maxcellnum = 1000;
        }
        

        /// <summary>
        /// 播放下一格
        /// </summary>
        public void PlayNext()
        {
            _newcell++;
            //触发所有颜色额开始循环的事件
            if (_newcell > _cellCount)
            {
                _newcell = 1;
            }
            if (_newcell == 1)
            {
                foreach (var V in _manager.Metronomemanage)
                {
                    V.Key.EventManager.Dispatch(TimbreEvent.BeginLoop);
                }
            }
           
            foreach (var V in _manager.Metronomemanage)
            {
                var cell =  V.Value.GetNextCell();
                cell.Play(V.Key);
            }

            
            //触发所有颜色额结束循环的事件
            if (_newcell == _cellCount)
            {
                foreach (var V in _manager.Metronomemanage)
                {
                    V.Key.EventManager.Dispatch(TimbreEvent.EndLoop);
                }
            }
        }
        
        /// <summary>
        /// 添加新的音色与节点链表
        /// </summary>
        /// <param name="timbre"></param>
        public void AddTimbre(IMetronomUI ui,ITimbre timbre)
        {
            if (_manager.RegisterTimbre(timbre))
            {
                _timbreCount++;
                for (int i = 0; i < _cellCount; i++)
                {
                    var cell =  _manager.Metronomemanage[timbre].AddCell();
                    ui.AddCell(timbre,cell);
                }
            }
        }


        /// <summary>
        /// 删除一个音色
        /// </summary>
        /// <param name="timbre">音色</param>
        public void DeleteTimbre(IMetronomUI ui,ITimbre timbre)
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
        public void AddCell(int num,IMetronomUI ui)
        {
            if (_maxcellnum < _cellCount + num)
            {
                Debug.LogError("节点数超过最大负荷");
                return;
            }
            for (int i = 0; i < num; i++)
            {
                AddCell(ui);
            }
        }
    
        /// <summary>
        /// 加节点一次添加一个
        /// </summary>
        public void AddCell(IMetronomUI ui)
        {
            if (_maxcellnum < _cellCount + 1)
            {
                Debug.LogError("节点数超过最大负荷");
                return;
            }
            foreach (var v in _manager.Metronomemanage)
            {
                var cell =  v.Value.AddCell();
                ui.AddCell(v.Key,cell);
            }
            _cellCount++;
        }
        
        /// <summary>
        /// 移除节点
        /// </summary>
        public void RemoveCell(IMetronomUI ui)
        {
            if (_maxcellnum == 0)
            {
                Debug.LogError("移除节点数太多");
            }
            foreach (var v in _manager.Metronomemanage)
            {
                var t = v.Value.RemoveCell();
                ui.RemoveCell(v.Key,t);
            }
            _cellCount--;
        }

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="num">数量</param>
        public async void RemoveCell(int num,IMetronomUI ui)
        {
            if (_cellCount - num < 0)
            {
                Debug.LogError("移除节点数太多");
                return;
            }
            for (int i = 0; i < num; i++)
            {
                await UniTask.DelayFrame(i);
                RemoveCell(ui);
            }
        }
    }
    
    
}