using System;
using GameTools.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTools.MonoTool.Player
{
    public class PlayerContronal : AbsBaseUpdateOnBeat
    {
        private float _speed = 1.25f;
        public float Speed => _speed;


        public Sprite _first;
        public Sprite _second;

        private int _imageid = 0;

        private bool _isDie;

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
            if (_isDie) return;
            Debug.Log("死了");
            SceneManager.Reset();
            Destroy(gameObject);
            _isDie = true;
        }
    }
}