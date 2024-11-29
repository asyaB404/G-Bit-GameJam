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
    
    private PlayManage _playManage;
    public PlayManage PlayManage => _playManage;

    //人物控制器
    private PlayerContronal _player;
    public PlayerContronal Player => _player;


    //BPM数值
    [SerializeField, Header("BPM数值")] private int BPM;

    [SerializeField] private float offset;

    [Header("鼓点器格子数"), SerializeField] private int CellNum = 8;

    //音色实例
    private readonly List<Timbre_Common> _timbre = new List<Timbre_Common>();

    //场景中的工具
    private Abs_Tool[] _tools;
    private IUpdateOnBeat[] _updateOnBeats;

    private int _newtimbre = 0;
    private List<Transform> sprite = new List<Transform>();

    void Awake()
    {
        //控制器
        _playManage = new PlayManage();
        _player = FindObjectOfType<PlayerContronal>();

        //添加格子
        _playManage.AddCell(CellNum);

        //找到场景中工具(平台)
        _tools = FindObjectsOfType<Abs_Tool>();
        _updateOnBeats = FindObjectsOfType<AbsBaseUpdateOnBeat>();
        foreach (var item in _updateOnBeats)
        {
            _playManage.EventManager.AddListener(PlayEvent.OnHitsBefore, item.UpdateOnBeat);
        }
        _playManage.EventManager.AddListener(PlayEvent.OnHitsBefore, AAA);
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
            _playManage.AddTimbre(t);
            Debug.Log("成功添加音色和绑定工具" + t);
            return;
        }

        Debug.Log("之前已添加音色只增加绑定工具");
    }

    private void Start()
    {
        StartB.onClick.AddListener((() => { play(); }));
    }

    async void play()
    {
        await UniTask.WaitForSeconds(0.2f);
        var a = AudioManager.Instance.PlaySound(Music);
        StartCoroutine(_playManage.Play(BPM, a));
        
        
        
        var p = _playManage.UIManage.Main.transform;
        var c = _playManage.UIManage.Main.transform.parent;
        for (int i = 0; i < p.childCount; i++)
        {
            sprite.Add(Instantiate(g, c).transform);
        }
        for (int i = 0; i < p.childCount; i++)
        {
            sprite[i].position = p.GetChild(i).GetChild(_newtimbre).transform.position;
        }
        
        
    }

    void AAA()
    {
        var p = _playManage.UIManage.Main.transform;
        for (int i = 0; i < p.childCount; i++)
        {
            sprite[i].position = p.GetChild(i).GetChild(_newtimbre).transform.position;
        }

        _newtimbre = (_newtimbre + 1) % CellNum;
//        Debug.Log(_newtimbre + "WWWWWWWWWWWWWWWWWWWWWWWWWWWW");
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

    public int Bpm
    {
        get => BPM;
        set => BPM = value;
    }

    #endregion
}