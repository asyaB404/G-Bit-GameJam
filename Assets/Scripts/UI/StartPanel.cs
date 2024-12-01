// // ********************************************************************************************
// //     /\_/\                           @file       StartPanel.cs
// //    ( o.o )                          @brief     G-Bit-GameJam
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2024113021
// //   (___)___)                         @Copyright  Copyright (c) 2024, Basya
// // ********************************************************************************************


using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class StartPanel : MonoBehaviour
    {
        [SerializeField] private Button[] btns;
        [SerializeField] private GameObject startAnim;
        [SerializeField] private GameObject zuoze;

        private void Awake()
        {
            btns[0].onClick.AddListener(StartGame);
            btns[1].onClick.AddListener(() => { zuoze.SetActive(!zuoze.activeSelf);}); 
            btns[2].onClick.AddListener(Application.Quit);
        }

        public async void StartGame()
        {
            startAnim.SetActive(true);
            await UniTask.WaitForSeconds(11.9f);
            SceneManager.NextLevel();
        }
    }
}