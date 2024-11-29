// // ********************************************************************************************
// //     /\_/\                           @file       Elevator.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112416
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using DG.Tweening;
using GameTools.MonoTool.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameTools.MonoTool
{
    public class Elevator : Abs_Tool
    {
        private float height = 5;

        
        private int maxStep = 4;

        [SerializeField] private int step = 0;
       

        public override void StartTouch(PlayerContronal player)
        {
            player.transform.SetParent(transform);
        }

        public override void EndTouch(PlayerContronal player)
        {
            player.transform.SetParent(null);
        }

        public override void Trigger()
        {
            // 确保 step 在 [0, maxStep-1] 内循环
           // step = (step + 1) % maxStep;
            if (step < maxStep / 2f)
            {
                // 前半部分上升
                transform.position += Vector3.up*height;
            }
            else
            {
                // 后半部分下降
                transform.position -= Vector3.up*height;
            }

            step = (step + 1) % maxStep;
        }
    }
}