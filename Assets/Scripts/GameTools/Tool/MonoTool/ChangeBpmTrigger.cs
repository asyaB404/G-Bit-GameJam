// // // ********************************************************************************************
// // //     /\_/\                           @file       ChangeBpmTrigger.cs
// // //    ( o.o )                          @brief     G-Bit-GameJam
// // //     > ^ <                           @author     Basya
// // //    /     \
// // //   (       )                         @Modified   2024112414
// // //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // // ********************************************************************************************
//
// using GameTools.MonoTool.Player;
// using UnityEngine;
//
// namespace GameTools.MonoTool
// {
//     public class ChangeBpmTrigger : Abs_Tool
//     {
//         [SerializeField, Tooltip("变换的倍数,推荐为整数倍")]
//         private float changeBpmMul = 2;
//
//         public override void StartTouch(PlayerContronal player)
//         {
//             Debug.LogWarning("ww");
//             GameContronal.Instance.Bpm *= (int)changeBpmMul;
//             GameContronal.Instance.PlayManage.Stop();
//             GameContronal.Instance.PlayManage.Play(GameContronal.Instance.Bpm);
//         }
//
//         public override void EndTouch(PlayerContronal player)
//         {
//             
//         }
//
//         public override void Trigger()
//         {
//         }
//     }
// }