
using GameTools.MonoTool;
using Metronome.timbre;
using UnityEngine;

public class Level_DLC : MonoBehaviour
{
    public Timbre_SO so_1;
    public Timbre_SO so_2;
    void Start()
    {
        var game = GameContronal.Instance;
        game.AddTimbre<Platform>(new Timbre_Common(so_1)); 
        game.AddTimbre<PlayerAttack>(new Timbre_Common(so_2));
    }
   
}