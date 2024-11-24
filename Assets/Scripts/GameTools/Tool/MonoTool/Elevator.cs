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
        [SerializeField] private float height;

        [FormerlySerializedAs("step")] [SerializeField]
        private int maxStep = 4;

        [SerializeField] private int step = 0;
        private float _initialY;

        private void Awake()
        {
            _initialY = transform.position.y;
        }

        public override void Touch(PlayerContronal player)
        {
        }

        public override void Trigger()
        {
            // 确保 step 在 [0, maxStep-1] 内循环
            step = (step + 1) % maxStep;

            // 计算目标高度
            float targetY;
            if (step <= maxStep / 2f)
            {
                // 前半部分上升
                targetY = _initialY + (height / (maxStep / 2f)) * step;
            }
            else
            {
                // 后半部分下降
                targetY = _initialY + height - (height / (maxStep / 2f)) * (step - maxStep / 2f);
            }

            // 使用 DOTween 平滑移动
            transform.DOMoveY(targetY, 60f / GameContronal.Instance.Bpm).SetEase(Ease.InOutQuad);
        }
    }
}