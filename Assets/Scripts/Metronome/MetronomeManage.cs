using System.Collections.Generic;
using System.Collections.ObjectModel;
using Metronome.timbre;
using UnityEngine;

namespace Metronome
{
    public class MetronomeManager
    {
        private Dictionary<ITimbre,CellQueue> _metronomemanage = new Dictionary< ITimbre,CellQueue>();
        public ReadOnlyDictionary<ITimbre,CellQueue> Metronomemanage => new ReadOnlyDictionary< ITimbre,CellQueue>(_metronomemanage);
        
        
        /// <summary>
        /// 把相应的音色和链表注册
        /// </summary>
        /// <param name="timbre">音色</param>
        /// <returns></returns>
        public bool RegisterTimbre(ITimbre timbre)
        {
            return RegisterTimbre(timbre, new CellQueue(_metronomemanage.Count));
        }
        
        /// <summary>
        /// 注册新音色
        /// </summary>
        /// <param name="timbre">音色</param>
        /// <param name="queue">队列</param>
        /// <returns></returns>

        public bool RegisterTimbre(ITimbre timbre, CellQueue queue)
        {
            if (_metronomemanage.ContainsKey(timbre))
            {
                Debug.LogWarning($"已经注册了{timbre}该音色如想注册重复请使用其他方法");
                return false;
            }
            _metronomemanage.Add(timbre,queue);
            return true;
        }

        /// <summary>
        /// 删除一个音色
        /// </summary>
        /// <param name="timbre">音色</param>
        /// <returns></returns>
        public bool DelectTimbre(ITimbre timbre)
        {
            if (!_metronomemanage.ContainsKey(timbre))
            {
                Debug.LogWarning($"无法删除{timbre}，因为不存在");
                return false;
            }
            _metronomemanage.Remove(timbre);
            return true;
        }


        
        /// <summary>
        /// 替换新音色
        /// </summary>
        /// <param name="beforetimbre">之前的音色</param>
        /// <param name="newtimbre">新的音色</param>
        /// <returns></returns>
        public bool RePlaceTimbre(ITimbre beforetimbre,ITimbre newtimbre)
        {
            if (_metronomemanage.ContainsKey(newtimbre))
            {
                Debug.LogError($"在替换过程中{newtimbre}音色已经被注册");
            }
            if (UnRegisterTimbre(beforetimbre, out CellQueue queue))
            {
                RegisterTimbre(newtimbre,queue);
                return true;
            }
            Debug.LogError($"在替换过程中未找到{beforetimbre}音色");
            return false;
        }
        
        /// <summary>
        /// 解绑定音色和链表，
        /// </summary>
        /// <param name="timbre">音色</param>
        /// <param name="queue">链表</param>
        /// <returns></returns>
        private bool UnRegisterTimbre(ITimbre timbre,out CellQueue queue)
        {
            if (_metronomemanage.ContainsKey(timbre))
            {
                queue = _metronomemanage[timbre];
                _metronomemanage.Remove(timbre);
                return true;
            }
            queue = null;
            return false;
        }
    }
}