using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;

public class Success : MonoBehaviour
{
    private async void OnTriggerEnter2D(Collider2D other)
    {
        NextLevelPanel.Instance.NextLevelAnim(1.2f);
        await UniTask.WaitForSeconds(3);
        SceneManager.NextLevel();
    }
}