using GameTools.MonoTool.Player;
using UnityEngine;

namespace GameTools.MonoTool
{
    public abstract class Abs_Tool:MonoBehaviour,IGameTool,ICanTouch,ICanTrigger
    {
        public abstract void Touch(PlayerContronal player);
        public abstract void Trigger();
    }
}