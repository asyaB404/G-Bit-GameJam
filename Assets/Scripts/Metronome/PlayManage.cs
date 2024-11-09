using Cysharp.Threading.Tasks;
using Metronome;
using Metronome.timbre;
using UnityEngine;

public class PlayManage 
{
    //是否暂停
    private bool _ispaused = false;
    public bool IsPaused => _ispaused;
    
    //是否在游戏中
    private bool _isplaying = false;
    public bool IsPlaying => _isplaying;
    
    /// <summary>
    /// 节拍器逻辑层面控制器
    /// </summary>
    private MetronomeModelController _controller;
    public MetronomeModelController Controller => _controller;

    /// <summary>
    /// Ui控制器
    /// </summary>
    private UIManage _uimanage;
    public UIManage UIManage => _uimanage;

    
    /// <summary>
    /// 使用控制器构造
    /// </summary>
    /// <param name="controller"></param>
    public PlayManage(MetronomeModelController controller,UIManage uimanage)
    {
        _controller = controller;
        _uimanage = uimanage;
    }
    
    /// <summary>
    /// 直接构造
    /// </summary>
    public PlayManage()
    {
        _controller = new MetronomeModelController();
        _uimanage = new UIManage();
    }
    
    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="bpm">BPM</param>
    
    public async void Play(int bpm)
    {
        if (_isplaying)
        {
            Debug.LogWarning("Already playing");
            return;
        }
        _isplaying = true;
        float timestep = 60f / bpm;

        //触发所有开始播放音色的事件
        foreach (var V in _controller.Manager.Metronomemanage)
        {
            V.Key.EventManager.Dispatch(TimbreEvent.BeginPlay);
        }
        
        while (true)
        {
            if (!_isplaying)
            {
                break;
            }
            if (_ispaused)
            {
                await UniTask.WaitUntil(() => !_ispaused);
            }
            await UniTask.WaitForSeconds(timestep);
            _controller.PlayNext();
        //    Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        }
        
        foreach (var V in _controller.Manager.Metronomemanage)
        {
            V.Key.EventManager.Dispatch(TimbreEvent.EndPlay);
        }
    }

    
    /// <summary>
    /// 暂停
    /// </summary>
    public void Puase()
    {
        _ispaused = true;
    }
    
    
    /// <summary>
    /// 继续
    /// </summary>
    public void Continue()
    {
        _ispaused = false;
    }

    /// <summary>
    /// 停止播放
    /// </summary>
    public void Stop()
    {
        _isplaying = false;
        _ispaused = false;
    }


    public void AddTimbre(ITimbre timbre)
    {
        _uimanage.AddTimbre(timbre);
        _controller.AddTimbre(_uimanage,timbre);
        
        if (IsPlaying)
        {
            timbre.EventManager.Dispatch(TimbreEvent.BeginPlay);
        }
        
    }

    public void DelectTimbre(ITimbre timbre)
    {
        _controller.DeleteTimbre(_uimanage,timbre);
        _uimanage.DeleteTimbre(timbre);
        
        if (IsPlaying)
        {
            timbre.EventManager.Dispatch(TimbreEvent.EndPlay);
        }
        
    }

    public void ReplaceTimbre(ITimbre beforetimbre, ITimbre newtimbre)
    {
        beforetimbre.EventManager.Dispatch(TimbreEvent.EndPlay);
        newtimbre.EventManager.Dispatch(TimbreEvent.BeginPlay);
        _uimanage.ReplaceTimbre(beforetimbre,newtimbre);
        _controller.ReplaceTimbre(beforetimbre,newtimbre);
    }

    public void AddCell(int num)
    {
        _controller.AddCell(num,_uimanage);
    }

    public void RemoveCell(int num)
    {
        _controller.RemoveCell(num,_uimanage);
    }
}
