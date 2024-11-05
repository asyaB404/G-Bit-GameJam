using System;

namespace Metronome
{
    public struct CellInfo
    {
        public CellInfo(Type timbre, int id)
        {
            _id = id;
            _timbre = timbre;
        }
        
        
        //音色
        private Type _timbre;
        public Type Timbre => _timbre;
        
        //同一个音色id排序
        private int _id;
        public int Id => _id;


        
    }
}