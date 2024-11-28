
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

    [Header("音乐")]
    public AudioClip Music;

    #endregion
    
    private PlayManage _playManage;
    public PlayManage PlayManage => _playManage;

    //人物控制器
    private PlayerContronal _player;
    public PlayerContronal Player => _player;
    
    
    //BPM数值
    [SerializeField, Header("BPM数值")] 
    private int BPM;

    [SerializeField] private float offset;
    
    [Header("鼓点器格子数"),SerializeField]
    private int CellNum =  8;
    
    //音色实例
    private readonly List<Timbre_Common> _timbre = new List<Timbre_Common>();

    //场景中的工具
    private List<Abs_Tool> _tools;
    
    void Awake()
    {
        //控制器
        _playManage = new PlayManage();
        _player = FindObjectOfType<PlayerContronal>();
        
        //添加格子
        _playManage.AddCell(CellNum);

        //找到场景中工具(平台)
        _tools = GameObject.FindObjectsOfType<Abs_Tool>().ToList();
        
        //添加人物移动的事件
        _playManage.EventManager.AddListener(PlayEvent.OnHitsBefore,
            (() =>
            {
                Player.transform.position = new Vector3(_player.transform.position.x + _player.Speed,
                    _player.transform.position.y, _player.transform.position.z);
            }));
    }

    /// <summary>
    /// 添加音色和绑定的工具
    /// </summary>
    /// <param name="t">音色</param>
    /// <typeparam name="T">工具种类</typeparam>
    public void AddTimbre<T>(Timbre_Common t) where T:Abs_Tool
    {
        foreach (var to in _tools)
        {
            if (to is T)
            {
                t.EventManager.AddListener(TimbreEvent.AfterHit,to.Trigger);
            }
        }

        if (!_timbre.Contains(t))
        {
            _timbre.Add(t);
            _playManage.AddTimbre(t);
            Debug.Log("成功添加音色和绑定工具"+t);
            return;
        }
        Debug.Log("之前已添加音色只增加绑定工具");
    }

    private void Start()
    {
        StartB.onClick.AddListener((() =>
        {
            play();
        }));
    }

    async void play()
    {
        await UniTask.WaitForSeconds(1);
        var a = AudioManager.Instance.PlaySound(Music);
        StartCoroutine(_playManage.Play(BPM,a));
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