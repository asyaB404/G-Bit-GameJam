using GameTools.MonoTool.Player;
using UnityEngine;

namespace GameTools.MonoTool
{
    public class Platform:Abs_Tool
    {
        [SerializeField,Header("门是否打开")]
        private bool _isopen;
        public override void StartTouch(PlayerContronal player)
        {
            player.Die();
        }
        

        public override void EndTouch(PlayerContronal player)
        {
            
        }

        public override void Trigger()
        {
            if (!_isopen)
            {
                transform.position = new Vector3(transform.position .x,transform.position.y+5,transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position .x,transform.position.y-5,transform.position.z);
            }
            _isopen = !_isopen;
        }
    }
}