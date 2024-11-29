// // ********************************************************************************************
// //     /\_/\                           @file       Enemy.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112920
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using UnityEngine;

namespace GameTools.Enemy
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        public void TakeDamage()
        {
            Die();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}