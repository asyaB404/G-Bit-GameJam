// // ********************************************************************************************
// //     /\_/\                           @file       DragObject.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112312
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************

using UnityEngine;
using UnityEngine.EventSystems;

namespace Other
{
    [ExecuteAlways]
    public class EdgeDraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;


        [Tooltip("允许拖拽的边缘宽度")] [SerializeField]
        private float edgeWidth = 10f;

        private bool _isDragging = false;

        private Canvas CurCanvas
        {
            get
            {
                if (_canvas != null) return _canvas;
                _canvas = GetComponentInParent<Canvas>();
                if (_canvas == null)
                {
                    Debug.LogError("Canvas不见了");
                }

                return _canvas;
            }
        }
        
        private CanvasGroup CurCanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = GetComponent<CanvasGroup>();
                }

                return _canvasGroup;
            }
        }

        private RectTransform CurRectTransform
        {
            get
            {
                _rectTransform ??= (RectTransform)transform;
                return _rectTransform;
            }
        }
        

        // 判断点击是否在边缘区域
        private bool IsPointerOnEdge(Vector2 pointerPosition)
        {
            // 获取 UI 元素的局部坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(CurRectTransform, pointerPosition, null,
                out Vector2 localPoint);
            Rect rect = CurRectTransform.rect;

            // 判断是否在边缘区域内
            return Mathf.Abs(localPoint.x) >= rect.width / 2 - edgeWidth ||
                   Mathf.Abs(localPoint.y) >= rect.height / 2 - edgeWidth;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsPointerOnEdge(eventData.position)) return;
            _isDragging = true;
            if (CurCanvasGroup != null)
            {
                CurCanvasGroup.blocksRaycasts = false; // 拖拽时不阻挡射线
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging || CurCanvas == null) return;

            // 将拖拽位移转换为相对于 Canvas 的局部坐标
            CurRectTransform.anchoredPosition += eventData.delta / CurCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDragging && CurCanvasGroup != null)
            {
                CurCanvasGroup.blocksRaycasts = true; // 恢复射线阻挡
            }

            _isDragging = false;
        }

        private void OnDrawGizmos()
        {
            Color previousColor = Gizmos.color;
            // 设置绘制颜色
            Gizmos.color = Color.red;

            // 获取 RectTransform 的世界坐标矩形
            Vector3[] corners = new Vector3[4];
            CurRectTransform.GetWorldCorners(corners);

            // 分别计算四个内缩的边界点
            Vector3 topLeftOuter = corners[1]; // 左上
            Vector3 topRightOuter = corners[2]; // 右上
            Vector3 bottomRightOuter = corners[3]; // 右下
            Vector3 bottomLeftOuter = corners[0]; // 左下

            // 根据 edgeWidth 内缩，计算边缘的内边界
            Vector3 topLeftInner = topLeftOuter + new Vector3(edgeWidth, -edgeWidth, 0);
            Vector3 topRightInner = topRightOuter + new Vector3(-edgeWidth, -edgeWidth, 0);
            Vector3 bottomRightInner = bottomRightOuter + new Vector3(-edgeWidth, edgeWidth, 0);
            Vector3 bottomLeftInner = bottomLeftOuter + new Vector3(edgeWidth, edgeWidth, 0);

            // 绘制外部矩形边框
            Gizmos.DrawLine(topLeftOuter, topRightOuter);
            Gizmos.DrawLine(topRightOuter, bottomRightOuter);
            Gizmos.DrawLine(bottomRightOuter, bottomLeftOuter);
            Gizmos.DrawLine(bottomLeftOuter, topLeftOuter);

            // 绘制内部矩形边框
            Gizmos.DrawLine(topLeftInner, topRightInner);
            Gizmos.DrawLine(topRightInner, bottomRightInner);
            Gizmos.DrawLine(bottomRightInner, bottomLeftInner);
            Gizmos.DrawLine(bottomLeftInner, topLeftInner);

            Gizmos.color = previousColor;
        }
    }
}