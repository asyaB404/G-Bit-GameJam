using Unity.VisualScripting;
using UnityEngine;

namespace GameTools.MonoTool
{
    public class Platform:Abs_Tool
    {
        public override void Touch()
        {
            Debug.Log("触碰平台");
        }
    }
}