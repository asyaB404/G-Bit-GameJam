// // ********************************************************************************************
// //     /\_/\                           @file       VisibilityButtonUI.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112314
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************


using UnityEngine;
using UnityEngine.UI;

namespace Other
{
    public class VisibilityButtonUI : MonoBehaviour
    {
        [Tooltip("是否可见")] [SerializeField] private bool isVisible = true;

        [Tooltip("绑定的内容，如果为空默认为该按钮的父物体")] [SerializeField]
        private RectTransform content;

        private RectTransform Content
        {
            get
            {
                content ??= (RectTransform)transform.parent;
                return content;
            }
        }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            // 原来的位置
            var position = Content.position;
            if (isVisible)
            {
                // 1080需要替换为Canvas相对的分辨率
                position.y = (1080 + Content.sizeDelta.y) / 2;
            }
            else
            {
                position.y = 0;
            }

            Content.position = position;
            isVisible = !isVisible;
        }
    }
}