using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Global
{
    private static int Level = 0;

    public static void NextLevel()
    {
        SceneManager.LoadSceneAsync(++Level);
    }

    public static void R()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    
    
}
