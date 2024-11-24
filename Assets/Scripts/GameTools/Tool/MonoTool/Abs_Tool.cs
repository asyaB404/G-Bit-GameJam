using GameTools.MonoTool.Player;
using UnityEngine;

namespace GameTools.MonoTool
{
    public abstract class Abs_Tool:MonoBehaviour,IGameTool,ICanTouch,ICanTrigger
    {
        public abstract void StartTouch(PlayerContronal player);
        public abstract void EndTouch(PlayerContronal player);

        public abstract void Trigger();
    }
}