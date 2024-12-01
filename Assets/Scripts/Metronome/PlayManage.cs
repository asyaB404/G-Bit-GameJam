using System.Collections;
using System.Diagnostics;
using Metronome;
using Metronome.timbre;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

    private EventManager<PlayEvent> _eventManager = new EventManager<PlayEvent>();
    public EventManager<PlayEvent> EventManager => _eventManager;

    /// <summary>
    /// 使用控制器构造
    /// </summary>
    /// <param name="controller"></param>
    public PlayManage(MetronomeModelController controller, UIManage uimanage)
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
    public IEnumerator Play(int bpm, AudioSource musicSource)
    {
        //如果游戏已经开始返回
        if (_isplaying)
        {
            Debug.LogWarning("Already playing");
            //return;
            yield break;
        }

        //开始游戏
        _isplaying = true;
        double timestep = ((60d / bpm));
        //执行游戏开始等等全局事件
        _eventManager.Dispatch(PlayEvent.OnStartPlay);

        //触发所有开始播放音色的事件
        foreach (var V in _controller.Manager.Metronomemanage)
        {
            V.Key.EventManager.Dispatch(TimbreEvent.BeginPlay);
        }

        var _timer = timestep;

        while (true)
        {
            if (!_isplaying)
            {
                //执行游戏结束的事件
                _eventManager.Dispatch(PlayEvent.OnEndPlay);
                break;
            }

            if (_ispaused)
            {
                //执行游戏暂停的事件
                _eventManager.Dispatch(PlayEvent.OnPausePlay);

                //await UniTask.WaitUntil(() => !_ispaused);
                yield return new WaitUntil(() => !_ispaused);

                //执行游戏进行的事件
                _eventManager.Dispatch(PlayEvent.OnContinuePlay);
            }

            if (musicSource.isPlaying && musicSource.time >= _timer)
            {
                //执行游戏本节拍前的事件
                _eventManager.Dispatch(PlayEvent.OnHitsBefore);

                _controller.PlayNext();

                //执行游戏本节拍后的事件
                _eventManager.Dispatch(PlayEvent.OnHitsAfter);

                _timer += timestep;
            }

            yield return new WaitForNextFrameUnit();
        }

        //所有音色结束的事件
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
        _controller.AddTimbre(_uimanage, timbre);

        if (IsPlaying)
        {
            timbre.EventManager.Dispatch(TimbreEvent.BeginPlay);
        }
    }

    public void DelectTimbre(ITimbre timbre)
    {
        _controller.DeleteTimbre(_uimanage, timbre);
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
        _uimanage.ReplaceTimbre(beforetimbre, newtimbre);
        _controller.ReplaceTimbre(beforetimbre, newtimbre);
    }

    public void AddCell(int num)
    {
        _controller.AddCell(num, _uimanage);
    }

    public void RemoveCell(int num)
    {
        _controller.RemoveCell(num, _uimanage);
    }
}

public enum PlayEvent
{
    //关卡开始结束
    OnStartPlay,
    OnEndPlay,

    //游戏暂停
    OnPausePlay,
    OnContinuePlay,

    //当同一个节拍的所有音色发声之前
    OnHitsBefore,

    //当同一个节拍的所有音色发声之后
    OnHitsAfter,

    //一个循环开始和结束
    OnCycleEnd,
    OnCycleBegin,
}