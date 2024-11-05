using System.Collections;
using System.Collections.Generic;
using Metronome;
using Metronome.timbre;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Timbre_A a = new Timbre_A(null);
        Timbre_B b = new Timbre_B(null);    
        CellManager manager = new CellManager();
        manager.AddTimbre(a);
        manager.AddCell(4);
        foreach (var VARIABLE in manager.Timbres)
        {
            foreach (var VARIABLE2 in VARIABLE.Value.Cells)
                Debug.Log(VARIABLE2.Info.Id);
        }
        Debug.Log("_________________________________________________");
        manager.RemoveCell(2);
        foreach (var VARIABLE in manager.Timbres)
        {
            foreach (var VARIABLE2 in VARIABLE.Value.Cells)
                Debug.Log(VARIABLE2.Info.Id);
        }
        Debug.Log("_________________________________________________");
        manager.AddCell(4);
        foreach (var VARIABLE in manager.Timbres)
        {
            foreach (var VARIABLE2 in VARIABLE.Value.Cells)
                Debug.Log(VARIABLE2.Info.Id);
        }
        Debug.Log("[[[[[[[[[[[[[[[[[[[[[[[[[_");
        manager.AddTimbre(b);
        foreach (var VARIABLE in manager.Timbres)
        {
            foreach (var VARIABLE2 in VARIABLE.Value.Cells)
                Debug.Log(VARIABLE2.Info.Id);
        }
        Debug.Log("_________________________________________________");
        
    }

  
}
