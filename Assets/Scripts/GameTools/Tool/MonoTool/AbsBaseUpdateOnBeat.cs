// // ********************************************************************************************
// //     /\_/\                           @file       BaseUpdateOnBeat.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112920
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using UnityEngine;

namespace GameTools.MonoTool
{
    public abstract class AbsBaseUpdateOnBeat : MonoBehaviour, IUpdateOnBeat
    {
        public abstract void UpdateOnBeat();
    }
}