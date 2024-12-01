// // ********************************************************************************************
// //     /\_/\                           @file       NextLevelPanel.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024120114
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class NextLevelPanel : MonoBehaviour
    {
        public static NextLevelPanel Instance { get; private set; }
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            Instance = this;
            canvasGroup.alpha = 0;
        }

        public void NextLevelAnim(float duration)
        {
            canvasGroup.DOFade(1, duration);
        }
    }
}