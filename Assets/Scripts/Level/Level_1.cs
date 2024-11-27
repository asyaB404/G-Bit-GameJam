using GameTools.MonoTool;
using Metronome.timbre;
using UnityEngine;

public class Level_1 : MonoBehaviour
{
    public Timbre_SO so;
    void Start()
    {
        var game = GameContronal.Instance;
        game.AddTimbre<Platform>(new Timbre_Common(so));
      //  game.AddTimbre<Elevator>(new Timbre_Common(so));
    }
    
}
