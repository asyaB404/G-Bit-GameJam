using System.Collections;
using System.Collections.Generic;
using GameTools.MonoTool;
using Metronome.timbre;
using UnityEngine;

public class Level_2 : MonoBehaviour
{
    public Timbre_SO so;
    public Timbre_SO so_1;
    void Start()
    {
        var game = GameContronal.Instance;
        game.AddTimbre<Platform>(new Timbre_Common(so)); 
        game.AddTimbre<Elevator>(new Timbre_Common(so_1));
    }
}
