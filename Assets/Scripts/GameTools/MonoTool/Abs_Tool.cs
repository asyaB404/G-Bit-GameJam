using UnityEngine;

namespace GameTools.MonoTool
{
    public abstract class Abs_Tool:MonoBehaviour,IGameTool,ICanTouch
    {
        public abstract void Touch();
    }
}