// // ********************************************************************************************
// //     /\_/\                           @file       PlayerAttack.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112920
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using GameTools.Enemy;
using GameTools.MonoTool.Player;
using UnityEngine;

namespace GameTools.MonoTool
{
    public class PlayerAttack : Abs_Tool
    {
        [SerializeField] private Vector3 origin;
        [SerializeField] private float rayLength = 1.5f; // 射线长度
        [SerializeField] private LayerMask enemyLayer; // 指定敌人所在的层

        public override void StartTouch(PlayerContronal player)
        {
        }

        public override void EndTouch(PlayerContronal player)
        {
        }

        public override void Trigger()
        {
            // 获取玩家的位置
            Vector2 origin = transform.position + this.origin;
            // 射线方向（X 轴正方向）
            Vector2 direction = Vector2.right;
            // 进行射线检测
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, enemyLayer);
            if (hit.collider != null)
            {
                IEnemy enemy = hit.collider.GetComponent<IEnemy>();
                if (enemy != null)
                {
                    Debug.Log("攻击命中敌人");
                    enemy.TakeDamage();
                }
            }

            // 可视化射线（调试用）
            _lastRayOrigin = origin;
            _lastRayDirection = direction;
        }

        #region DeBug

        private Vector2 _lastRayOrigin;
        private Vector2 _lastRayDirection;

        private void OnDrawGizmos()
        {
            if (_lastRayDirection == Vector2.zero) return;
            // 设置 Gizmos 的颜色
            Gizmos.color = Color.red;

            // 绘制射线
            Gizmos.DrawLine(_lastRayOrigin, _lastRayOrigin + _lastRayDirection * rayLength);
        }

        #endregion
    }
}