using Metronome.timbre;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Timbre_SO audioClip_0;
    public Timbre_SO audioClip_1;
    public Timbre_SO audioClip_2;

    public Button PlayButton;
    public Button PauseButton;
    public Button AddButton;
    public Button SubButton;
    public Slider BPMSlider;
    
    public Text BPMText;

    private int BPM = 60;
    
    private void Start()
    {
       var a = new Timbre_Common(audioClip_2);
       var b = new Timbre_Common(audioClip_0);
       var c = new Timbre_Common(audioClip_1);
       
       
       a.EventManager.AddListener(TimbreEvent.BeginPlay,(() => Debug.Log("开始使用V音色")));
       b.EventManager.AddListener(TimbreEvent.BeginPlay,(() => Debug.Log("开始使用B音色")));
       b.EventManager.AddListener(TimbreEvent.BegainHit,(() => Debug.Log("B使用之前的判断")));
       b.EventManager.AddListener(TimbreEvent.AfterHit,(() => Debug.Log("B使用后的判断")));
       b.EventManager.AddListener(TimbreEvent.BeginLoop,(() => Debug.Log("新的循环")));
       b.EventManager.AddListener(TimbreEvent.EndLoop,(() => Debug.Log("循环结束")));
       PlayManage mc = new PlayManage();
       mc.AddTimbre(b);
       mc.AddTimbre(a);
       mc.AddTimbre(c); 
       mc.AddTimbre(new Timbre_Common(audioClip_0)); 
       mc.AddCell(6);


       PlayButton.onClick.AddListener(() => mc.Play((int)BPM));
       PauseButton.onClick.AddListener(()=>mc.Stop());
       AddButton.onClick.AddListener(()=>mc.AddCell(3));
       SubButton.onClick.AddListener(()=>mc.RemoveCell(3));;
       
     //  mc.Play(200);
       
        
       Debug.Log("节点数 "+mc.Controller.CellCount);
       Debug.Log("音色数 "+mc.Controller.TimbreCount);
        
    }

    private void Update()
    {
        BPM = (int)BPMSlider.value;
        Debug.Log(BPM);
        BPMText.text = BPM.ToString();
    }
}
