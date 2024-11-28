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

    private const int InitialPoolSize = 8; //初始池大小

    private AudioManager()
    {
        _audioSourcePool = new Stack<AudioSource>(InitialPoolSize);
        _activeAudioSources = new List<AudioSource>();

        // 创建一个 GameObject 用于挂载 AudioSource，并保持在场景中
        _soundManagerObject = new GameObject("SoundManagerObject");
        Object.DontDestroyOnLoad(_soundManagerObject);

        // 预先生成一定数量的 AudioSource 供复用
        for (int i = 0; i < InitialPoolSize; i++)
        {
            _audioSourcePool.Push(CreateNewAudioSource());
        }
    }

    public AudioManager(List<AudioSource> activeAudioSources)
    {
        _activeAudioSources = activeAudioSources;
    }

    // 创建新的 AudioSource
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">要播放的音频片段</param>
    /// <param name="volume">音量大小（0.0 - 1.0）</param>
    public AudioSource PlaySound(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null)
        {
            Debug.LogWarning("尝试播放的音效为空！");
            return null;
        }

        AudioSource audioSource = GetAvailableAudioSource();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        //将AudioSource添加到活动列表中以便后续回收
        _activeAudioSources.Add(audioSource);

        //在音效播放完成后回收
        RecycleAudioSourceAsync(audioSource).Forget();

        return audioSource;
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

        // 重置 AudioSource 的状态
        audioSource.clip = null;

        // 从活动列表中移除并将其回收到池中
        _activeAudioSources.Remove(audioSource);
        _audioSourcePool.Push(audioSource);
    }

    #endregion
}
