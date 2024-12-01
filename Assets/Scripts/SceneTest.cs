// // ********************************************************************************************
// //     /\_/\                           @file       SceneTest.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024120114
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using UnityEngine;

namespace DefaultNamespace
{
    public class SceneTest:MonoBehaviour
    {
        [ContextMenu("Reset")]
        private void Fun()
        {
            SceneManager.Reset();
        }
    }
}