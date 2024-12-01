// // ********************************************************************************************
// //     /\_/\                           @file       Enemy.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112920
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

#nullable enable
using GameTools.MonoTool;
using GameTools.MonoTool.Player;
using UnityEngine;


namespace GameTools.Enemy
{
    public class Sheep : AbsBaseUpdateOnBeat, IEnemy
    {
        [SerializeField] private bool moveToLeft = true; // 是否往左
        [SerializeField] private float patrolDistance = 2f; // 巡逻距离

        private Vector3 _startPosition; // 起始点
        private bool _movingToTarget = true; // 当前是否朝目标方向移动
        private Vector3 _currentTarget; // 当前目标位置

        private void Start()
        {
            // 初始化起始点为当前位置
            _startPosition = transform.position;
            if (moveToLeft)
            {
                _currentTarget = _startPosition + Vector3.left * patrolDistance;
            }
            else
            {
                _currentTarget = _startPosition + Vector3.right * patrolDistance;
            }
        }

        public void StartTouch(PlayerContronal player)
        {
            gameObject.SetActive(false);
        }

        public void EndTouch(PlayerContronal player)
        {
            
        }

        public void TakeDamage()
        {
            Die();
        }

        public void Die()
        {
            SceneManager.Reset();
        }

        public override void UpdateOnBeat()
        {
            if (gameObject == null)
            {
                return;
            }
               
            // 计算目标位置
            var targetPosition = _movingToTarget ? _currentTarget : _startPosition;
            // 确定每次移动的方向
            var direction = _movingToTarget
                ? (moveToLeft ? Vector3.left : Vector3.right)
                : (moveToLeft ? Vector3.right : Vector3.left);
            transform.Translate(direction*1.25f, Space.World); // 在世界空间中按方向移动
            // 检查是否到达目标点并切换方向
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                _movingToTarget = !_movingToTarget;
            }
        }
    }
}