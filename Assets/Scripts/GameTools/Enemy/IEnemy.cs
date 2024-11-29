// // ********************************************************************************************
// //     /\_/\                           @file       IEnemy.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112920
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using UnityEngine;

namespace GameTools.Enemy
{
    public interface IEnemy
    {
        void TakeDamage();
        void Die();
    }
}