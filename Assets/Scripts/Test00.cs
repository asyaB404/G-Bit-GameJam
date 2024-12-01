// // ********************************************************************************************
// //     /\_/\                           @file       Test00.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024120120
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************


using UnityEngine;

public class Test00 : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    [ContextMenu("test")]
    private void Test()
    {
        AudioManager.Instance.PlaySound(audioClip);
    }


    [ContextMenu("reset")]
    private void Test1()
    {
        SceneManager.Reset();
    }
}