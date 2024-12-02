// // ********************************************************************************************
// //     /\_/\                           @file       Enemy.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024112920
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************


using GameTools.MonoTool;
using GameTools.MonoTool.Player;
using UnityEngine;


namespace GameTools.Enemy
{
    public class SoundSheep : AbsBaseUpdateOnBeat, IEnemy
    {
        [SerializeField] private AudioClip audioClip;

        public void StartTouch(PlayerContronal player)
        {
            gameObject.SetActive(false);
            AudioManager.Instance.PlaySound(audioClip);
        }

        public void EndTouch(PlayerContronal player)
        {
        }

        public void TakeDamage()
        {
            Die();
        }

        public void Die()
        {
            SceneManager.Reset();
        }

        public override void UpdateOnBeat()
        {
            // 不会动的小羊
        }
    }
}