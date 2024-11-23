using Unity.VisualScripting;
using UnityEngine;

namespace GameTools.MonoTool
{
    public class Platform:MonoBehaviour,IGameTool,ICanTouch
    {
        public void Touch()
        {
            Debug.Log("Platform touched");
        }
    }
}