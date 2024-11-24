using System;
using UnityEngine;

namespace GameTools.MonoTool.Player
{
    public class PlayerContronal:MonoBehaviour
    {
        private float _speed = 1.25f;
        public float Speed => _speed;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<Abs_Tool>(out var t))
            {
                t.StartTouch(this);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<Abs_Tool>(out var t))
            {
                t.EndTouch(this);
            }
        }
    }
}