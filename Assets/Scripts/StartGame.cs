using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    private ColorAdjustments m;
    private bool p;
    async void  Start()
    {
        var v =  GetComponent<Volume>();
        v.profile.TryGet<Vignette>(out var g);
        v.profile.TryGet<ColorAdjustments>(out  m);
        while (g.intensity.value > 0)
        {
            await UniTask.WaitForSeconds(0.05f);
            g.intensity.value -= 0.1f;
        }
        GameContronal.Instance.PlayManage.EventManager.AddListener(PlayEvent.OnHitsAfter,A);
        
    }

    void A()
    {
        if (p)
        {
            m.hueShift.value += 45;
        }
        else
        {
            m.hueShift.value -= 45;
        }
        
        if (m.hueShift.value >= m.hueShift.max||m.hueShift.value <= m.hueShift.min)
        {
            p = !p;
        }
    }
    
   
}
