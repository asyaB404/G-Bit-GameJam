using Cysharp.Threading.Tasks;
using Metronome;
using Metronome.timbre;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Timbre_SO audioClip;
    public Timbre_SO audioClip2;
    
    private async void Start()
    {
       var a = new Timbre_Common(audioClip2);
       var b = new Timbre_Common(audioClip);
       var v = new Timbre_Common(audioClip);
       var m = new Timbre_Common(audioClip2);
       
       v.EventManager.AddListener(TimbreEvent.BeginPlay,(() => Debug.Log("开始使用V音色")));
       b.EventManager.AddListener(TimbreEvent.BeginPlay,(() => Debug.Log("开始使用B音色")));
       b.EventManager.AddListener(TimbreEvent.BegainHit,(() => Debug.Log("B使用之前的判断")));
       b.EventManager.AddListener(TimbreEvent.AfterHit,(() => Debug.Log("B使用后的判断")));
       b.EventManager.AddListener(TimbreEvent.BeginLoop,(() => Debug.Log("新的循环")));
       b.EventManager.AddListener(TimbreEvent.EndLoop,(() => Debug.Log("循环结束")));
       PlayManage mc = new PlayManage();
       mc.AddTimbre(b);
     //  mc.AddTimbre(a);
       
       mc.AddCell(6);
       mc.Play(200);
       
        

      await UniTask.WaitForSeconds(5);
      mc.AddCell(6);
      mc.AddTimbre(v);
        
        
        Debug.Log("节点数 "+mc.Controller.CellCount);
        Debug.Log("音色数 "+mc.Controller.TimbreCount);
        
        
        
        
        // await UniTask.WaitForSeconds(3);
        // Debug.LogWarning("暂停中");
        // pm.Puase();
        //
        // Debug.LogWarning("删除音色1轨道");
        //
        // pm.Controller.DeleteTimbre(b);
        // await UniTask.WaitForSeconds(3);
        // Debug.LogWarning("继续播放");
        // pm.Continue();
        // await UniTask.WaitForSeconds(3);
        // pm.Stop();
        
    }
}
