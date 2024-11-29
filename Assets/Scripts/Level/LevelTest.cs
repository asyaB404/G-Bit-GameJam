// // ********************************************************************************************
// //     /\_/\                           @file       LevelTest.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112921
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using GameTools.MonoTool;
using Metronome.timbre;
using UnityEngine;

namespace Level
{
    public class LevelTest:MonoBehaviour

    {
        public Timbre_SO so;

        void Start()
        {
            var game = GameContronal.Instance;
            game.AddTimbre<PlayerAttack>(new Timbre_Common(so));
        }
    }
}