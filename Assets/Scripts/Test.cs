using Cysharp.Threading.Tasks;
using Metronome;
using Metronome.timbre;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Timbre_SO audioClip;
    // Start is called before the first frame update
    private async void Start()
    {
       // AudioManager.Instance.PlaySound(audioClip, 1);
        var a = new Timbre_Common(audioClip);
        MetronomeController mc = new MetronomeController();
        mc.AddTimbre(a);
        mc.AddCell(100);
        mc.AddTimbre(new Timbre_Common(audioClip));
        mc.RemoveCell(95);
        Debug.Log("节点数 "+mc.CellCount);
        Debug.Log("音色数 "+mc.TimbreCount);
        
        mc.ReplaceTimbre(a,new Timbre_Common(audioClip));
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
