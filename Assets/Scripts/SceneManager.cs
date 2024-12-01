using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManager
{
    private static int Level = 0;

    public static void NextLevel()
    {
        AudioManager.Instance.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(++Level);
    }

    public static void End()
    {
        Level = 0;
        AudioManager.Instance.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

    public static void Reset()
    {
        AudioManager.Instance.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    
    
}
