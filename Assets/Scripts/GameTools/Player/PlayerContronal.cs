using System;
using UnityEngine;

namespace GameTools.MonoTool.Player
{
    public class PlayerContronal:MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}