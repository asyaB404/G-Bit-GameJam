using System;
using GameTools.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace GameTools.MonoTool.Player
{
    public class PlayerContronal : AbsBaseUpdateOnBeat
    {
        private float _speed = 1.25f;
        public float Speed => _speed;


        public Sprite _first;
        public Sprite _second;

        private int _imageid = 0;

       float a = -0.3f;
       float b = 0.2f;
        private void Update()
        {
            var f =  Physics2D.OverlapCircle((Vector2)(transform.position-Vector3.down*a),b);
            if (f == null)
            {
                Die();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere((Vector2)(transform.position-Vector3.down*a),b);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<Abs_Tool>(out var t))
            {
                t.StartTouch(this);
            }
            if (other.transform.TryGetComponent<IEnemy>(out var tEnemy))
            {
                tEnemy.StartTouch(this);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<Abs_Tool>(out var t))
            {
                t.EndTouch(this);
            }
            if (other.transform.TryGetComponent<IEnemy>(out var tEnemy))
            {
                tEnemy.EndTouch(this);
            }
        }

        public override void UpdateOnBeat()
        {
            transform.position = new Vector3(transform.position.x + Speed,
                transform.position.y, transform.position.z);

            var s = _imageid switch
            {
                0 => _first,
                1 => _second,
                _ => throw new ArgumentOutOfRangeException()
            };
            _imageid = (++_imageid) % 2;
            GetComponent<SpriteRenderer>().sprite = s;
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}