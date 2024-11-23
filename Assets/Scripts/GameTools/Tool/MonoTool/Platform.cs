using GameTools.MonoTool.Player;
using UnityEngine;

namespace GameTools.MonoTool
{
    public class Platform:Abs_Tool
    {
        public override void Touch(PlayerContronal player)
        {
            Destroy(player.gameObject);
        }

        public override void Trigger()
        {
            if (transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position .x,5,transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position .x,-1,transform.position.z);
            }
        }
    }
}