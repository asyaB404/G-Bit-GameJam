// // ********************************************************************************************
// //     /\_/\                           @file       IEnemy.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112920
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using GameTools.MonoTool.Player;

namespace GameTools.Enemy
{
    public interface IEnemy
    {
        void StartTouch(PlayerContronal player);
        void EndTouch(PlayerContronal player);
        void TakeDamage();
    }
}