using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Success : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Global.NextLevel();
    }
}
