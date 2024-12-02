using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class AudioManager
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance ??= new AudioManager();

    private readonly Stack<AudioSource> _audioSourcePool; // 用于存储闲置的 AudioSource
    private readonly List<AudioSource> _activeAudioSources; // 当前正在使用的 AudioSource 
    private readonly GameObject _soundManagerObject; // 用于挂载 AudioSource 的 GameObject
    private readonly AudioSource _musicSource;
    private const int INITIAL_POOL_SIZE = 4; //初始池大小

    private AudioManager()
    {
        _audioSourcePool = new Stack<AudioSource>(INITIAL_POOL_SIZE);
        _activeAudioSources = new List<AudioSource>();

        // 创建一个 GameObject 用于挂载 AudioSource，并保持在场景中
        _soundManagerObject = new GameObject("SoundManagerObject");
        Object.DontDestroyOnLoad(_soundManagerObject);

        // 预先生成一定数量的 AudioSource 供复用
        for (int i = 0; i < INITIAL_POOL_SIZE; i++)
        {
            _audioSourcePool.Push(CreateNewAudioSource());
        }

        _musicSource = CreateNewAudioSource();
        _musicSource.loop = true; // 背景音乐通常循环播放
    }

    public AudioManager(List<AudioSource> activeAudioSources)
    {
        _activeAudioSources = activeAudioSources;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">要播放的音频片段</param>
    /// <param name="volume">音量大小（0.0 - 1.0）</param>
    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null)
        {
            Debug.LogWarning("尝试播放的音效为空！");
        }

        AudioSource audioSource = GetAvailableAudioSource();
        audioSource.enabled = true;
        if (audioSource.clip != null) Debug.LogWarning("bug");
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        //将AudioSource添加到活动列表中以便后续回收
        _activeAudioSources.Add(audioSource);

        //在音效播放完成后回收
        RecycleAudioSourceAsync(audioSource).Forget();
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="clip">要播放的音乐</param>
    /// <param name="volume">音量大小（0.0 - 1.0）</param>
    /// <returns>音乐组件</returns>
    public AudioSource PlayMusic(AudioClip clip, float volume = 1.0f)
    {
        _musicSource.clip = clip;
        _musicSource.volume = volume;
        _musicSource.Play();
        return _musicSource;
    }

    /// <summary>
    /// 清除所有当前播放的音效，停止并回收它们
    /// </summary>
    public void Clear()
    {
        _musicSource.clip = null;
        _musicSource.Stop();
        //遍历所有活动的 AudioSource，停止播放并回收
        foreach (var audioSource in _activeAudioSources)
        {
            audioSource.Stop();
            audioSource.clip = null;
            // _audioSourcePool.Push(audioSource); 会导致两次回收
        }

        _activeAudioSources.Clear();
    }

    #region Private

    private AudioSource CreateNewAudioSource()
    {
        AudioSource audioSource = _soundManagerObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        return audioSource;
    }

    // 获取可用的 AudioSource
    private AudioSource GetAvailableAudioSource()
    {
        // 如果池中没有可用的 AudioSource，则创建一个新的
        return _audioSourcePool.Count == 0 ? CreateNewAudioSource() : _audioSourcePool.Pop();
    }

    // 回收音效
    private async UniTaskVoid RecycleAudioSourceAsync(AudioSource audioSource)
    {
        // 等待音效播放完成
        await UniTask.WaitUntil(() => !audioSource.isPlaying);
        audioSource.clip = null;
        audioSource.enabled = false;
        // 从活动列表中移除并将其回收到池中
        _activeAudioSources.Remove(audioSource);
        _audioSourcePool.Push(audioSource);
    }

    #endregion
}