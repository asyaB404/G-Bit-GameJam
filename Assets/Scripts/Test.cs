using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Metronome;
using Metronome.timbre;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private async void Start()
    {
        var a = new Timbre_A(null);
        MetronomeController mc = new MetronomeController();
        mc.AddTimbre(a);
        mc.AddCell(100);
        mc.AddTimbre(new Timbre_B(null));
        mc.RemoveCell(95);
        Debug.Log("节点数 "+mc.CellCount);
        Debug.Log("音色数 "+mc.TimbreCount);
        
        mc.ReplaceTimbre(a,new Timbre_B(null));
        PlayManage pm = new PlayManage(mc);
        pm.Play(120);
        await UniTask.WaitForSeconds(3);
        pm.Puase();
        await UniTask.WaitForSeconds(3);
        pm.Continue();
        await UniTask.WaitForSeconds(3);
        pm.Stop();
    }
}
