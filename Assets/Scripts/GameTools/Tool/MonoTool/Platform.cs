using GameTools.MonoTool.Player;
using UnityEngine;

namespace GameTools.MonoTool
{
    public class Platform:Abs_Tool
    {
        private bool _isopen;
        public override void Touch(PlayerContronal player)
        {
            Destroy(player.gameObject);
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