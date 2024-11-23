using System;
using UnityEngine;

namespace GameTools.MonoTool.Player
{
    public class PlayerContronal:MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<Abs_Tool>(out var t))
            {
                t.Touch(this);
            }
        }
    }
}