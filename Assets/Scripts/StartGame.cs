using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    async void  Start()
    {
        var v =  GetComponent<Volume>();
        v.profile.TryGet<Vignette>(out var g);
        while (g.intensity.value > 0)
        {
            await UniTask.WaitForSeconds(0.05f);
            g.intensity.value -= 0.1f;
        }
        
        
    }

   
}
