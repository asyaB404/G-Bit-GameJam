// // ********************************************************************************************
// //     /\_/\                           @file       End.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024120112
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Level
{
    public class End : MonoBehaviour
    {
        private async void Start()
        {
            await UniTask.WaitForSeconds(7f);
            SceneManager.End();
        }
    }
}