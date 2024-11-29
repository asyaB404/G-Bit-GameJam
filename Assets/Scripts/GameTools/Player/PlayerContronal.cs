using System;
using UnityEngine;

namespace GameTools.MonoTool.Player
{
    public class PlayerContronal:MonoBehaviour
    {
        private float _speed = 1.25f;
        public float Speed => _speed;
        
        
        public Sprite _first;
        public Sprite _second;
        
        private int _imageid = 0;
        
        public void Trigger()
        {
            transform.position = new Vector3(transform.position.x + Speed, 
                transform.position.y, transform.position.z);
            
            var s = _imageid switch
            {
                0 => _first,
                1 => _second
            };
            _imageid = (++_imageid) % 2;
            GetComponent<SpriteRenderer>().sprite = s;
            Debug.Log(s);
        }
        
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