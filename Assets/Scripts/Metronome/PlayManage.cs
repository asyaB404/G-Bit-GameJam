using Cysharp.Threading.Tasks;
using Metronome;
using UnityEngine;

public class PlayManage 
{
    private bool _ispaused = false;
    public bool IsPaused => _ispaused;
    
    private bool _isplaying = false;
    public bool IsPlaying => _isplaying;
    
    private MetronomeController _controller;
    public MetronomeController Controller => _controller;

    public PlayManage(MetronomeController controller)
    {
        _controller = controller;
    }
    
    public async void Play(int bpm)
    {
        if (_isplaying)
        {
            Debug.LogWarning("Already playing");
            return;
        }
        _isplaying = true;
        float timestep = 60f / bpm;
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
            Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        }
    }

    public void Puase()
    {
        _ispaused = true;
    }
    
    public void Continue()
    {
        _ispaused = false;
    }

    public void Stop()
    {
        _isplaying = false;
        _ispaused = false;
    }
}
