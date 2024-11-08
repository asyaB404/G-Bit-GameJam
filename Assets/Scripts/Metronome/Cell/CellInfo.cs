using System;

namespace Metronome
{
    public struct CellInfo
    {
        public CellInfo(int id)
        {
            _id = id;
        }
        
        //同一个音色id排序
        private int _id;
        public int Id => _id;
        
    }
}