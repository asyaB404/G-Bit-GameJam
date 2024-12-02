using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameTools.MonoTool;
using GameTools.MonoTool.Player;
using Metronome.timbre;
using UnityEngine;
using UnityEngine.UI;

public class GameContronal : MonoBehaviour
{
    #region 测试使用

    [Header("开始")] public Button StartB;

    [Header("音乐")] public AudioClip Music;

    #endregion

    public GameObject g;

    public PlayManage PlayManage { get; private set; }

    //人物控制器
    public PlayerContronal Player { get; private set; }

    [SerializeField, Header("BPM数值")] private double BPM;

    [SerializeField] private int offset = 20;

    [Header("鼓点器格子数"), SerializeField] private int CellNum = 8;

    //音色实例
    private readonly List<Timbre_Common> _timbre = new List<Timbre_Common>();

    //场景中的工具
    private Abs_Tool[] _tools;
    private AbsBaseUpdateOnBeat[] _updateOnBeats;

    private int _newTimbre = 0;
    private readonly List<Transform> _sprites = new List<Transform>();

    private void Awake()
    {
        //控制器
        PlayManage = new PlayManage();
        Player = FindObjectOfType<PlayerContronal>();

        //添加格子
        PlayManage.AddCell(CellNum);

        //找到场景中工具(平台)
        _tools = FindObjectsOfType<Abs_Tool>();
        _updateOnBeats = FindObjectsOfType<AbsBaseUpdateOnBeat>();
        foreach (var item in _updateOnBeats)
        {
            PlayManage.EventManager.AddListener(PlayEvent.OnHitsBefore, item.UpdateOnBeat);
        }

        PlayManage.EventManager.AddListener(PlayEvent.OnHitsBefore, TimbreMoveAnim);
    }

    /// <summary>
    /// 添加音色和绑定的工具
    /// </summary>
    /// <param name="t">音色</param>
    /// <typeparam name="T">工具种类</typeparam>
    public void AddTimbre<T>(Timbre_Common t) where T : Abs_Tool
    {
        foreach (var to in _tools)
        {
            if (to is T)
            {
                t.EventManager.AddListener(TimbreEvent.BegainHit, to.Trigger);
            }
        }

        if (!_timbre.Contains(t))
        {
            _timbre.Add(t);
            PlayManage.AddTimbre(t);
            Debug.Log("成功添加音色和绑定工具" + t);
            return;
        }

        Debug.Log("之前已添加音色只增加绑定工具");
    }

    private void Start()
    {
        StartB.onClick.AddListener(Play);
    }

    private async void Play()
    {
        PlayManage.UIManage.Main.transform.parent.GetChild(2).gameObject.SetActive(true);
        AudioSource mainmusic = null;
        if (Music != null)
        {
            mainmusic = AudioManager.Instance.PlayMusic(Music);
        }

        var p = PlayManage.UIManage.Main.transform;
        var c = PlayManage.UIManage.Main.transform.parent;
        for (int i = 0; i < p.childCount; i++)
        {
            _sprites.Add(Instantiate(g, c).transform);
        }

        for (int i = 0; i < p.childCount; i++)
        {
            _sprites[i].position = p.GetChild(i).GetChild(_newTimbre).transform.position;
        }
        await UniTask.Delay(offset); 
        StartCoroutine(PlayManage.Play(BPM, mainmusic));
    }

    private void TimbreMoveAnim()
    {
        var p = PlayManage.UIManage.Main.transform;
        for (int i = 0; i < p.childCount; i++)
        {
            _sprites[i].position = p.GetChild(i).GetChild(_newTimbre).transform.position;
        }

        _newTimbre = (_newTimbre + 1) % CellNum;
    }


    #region basya

    private static GameContronal _instance;

    public static GameContronal Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<GameContronal>();
            if (_instance != null) return _instance;
            GameObject newInstance = new GameObject("GameContronal");
            _instance = newInstance.AddComponent<GameContronal>();
            return _instance;
        }
    }

    public double Bpm
    {
        get => BPM;
        set => BPM = value;
    }

    #endregion
}