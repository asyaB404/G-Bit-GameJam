using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Metronome
{
    public class CellQueue
    {
        
        /// <summary>
        /// 节点链表
        /// </summary>
        private  LinkedList<MCell> _celllist = new LinkedList<MCell>();
        public ReadOnlyCollection<MCell> CellList => _celllist.ToList().AsReadOnly();
        
        /// <summary>
        /// 当前节点
        /// </summary>
        private LinkedListNode<MCell> _currentCellNode;
        public LinkedListNode<MCell> CurrentCellNode => _currentCellNode;

        /// <summary>
        /// 链表的索引号ID
        /// </summary>
        private int _currentCellIndex;
        public int CurrentCellIndex => _currentCellIndex;

        /// <summary>
        /// 请输入该链表的索引
        /// </summary>
        /// <param name="index"></param>
        public CellQueue(int index)
        {
            _currentCellIndex = index;
        }
        
        /// <summary>
        /// 获取MCell单元
        /// </summary>
        /// <returns></returns>
        public MCell GetNextCell()
        {
            //当链表为空
            if (_celllist.Count == 0)
            {
                Debug.LogWarning("No more cells");
                return null;
            }
            
            //当链表不为空但是——currentCellNode为空
            if (_currentCellNode == null)
            {
                _currentCellNode = _celllist.First;
                return _celllist.First.Value;
            }
            
            //当链表已经到结尾时
            if (_currentCellNode.Next == null)
            {
                _currentCellNode = _celllist.First;
                return _currentCellNode.Value;
            }
            _currentCellNode = _currentCellNode.Next;
            return _currentCellNode.Value;
        }

        /// <summary>
        /// 向节点添加
        /// </summary>
        public MCell AddCell()
        {
            var a = _celllist.AddLast(new MCell(new CellInfo(_celllist.Count)));
            return a.Value;
        }

        /// <summary>
        /// 将节点删除
        /// </summary>
        public MCell RemoveCell()
        {
            var a = _celllist.Last.Value;
            _celllist.RemoveLast();
            return a;
        }
    }
}