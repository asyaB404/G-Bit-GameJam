using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameTools.MonoTool;
using GameTools.MonoTool.Player;
using Metronome.timbre;
using UnityEngine;
using UnityEngine.UI;

public class GameContronal : MonoBehaviour
{
    [Header("开始")] public Button StartB;

    private PlayManage _playManage;
    public PlayManage PlayManage => _playManage;

    //人物控制器
    private PlayerContronal _player;
    public PlayerContronal Player => _player;
    
    
    //BPM数值
    [SerializeField, Header("BPM数值")] private int BPM;
    
    //音色列表
    [Header("音色的列表")] public List<Timbre_SO> audioChilps;

    //音色实例
    private List<Timbre_Common> _timbre = new List<Timbre_Common>();

    //场景中的工具
    private List<Abs_Tool> _tools;

    void Awake()
    {
        //控制器
        _playManage = new PlayManage();
        _player = FindObjectOfType<PlayerContronal>();

        //实例化音色
        foreach (var V in audioChilps)
        {
            _timbre.Add(new Timbre_Common(V));
        }

        //找到场景中工具(平台)
        _tools = GameObject.FindObjectsOfType<Abs_Tool>().ToList();
        Debug.Log(_tools.Count);
    }

    private void Start()
    {
        StartB.onClick.AddListener((() => { _playManage.Play(BPM); }));
        _timbre[0].EventManager.AddListener(TimbreEvent.AfterHit, (() =>
        {
            foreach (var p in _tools)
            {
                p.Trigger();
            }
        }));
        _playManage.EventManager.AddListener(PlayEvent.OnHitsBefore,
            (() =>
            {
                Player.transform.position = new Vector3(_player.transform.position.x + _player.Speed,
                    _player.transform.position.y, _player.transform.position.z);
            }));


        _playManage.AddTimbre(_timbre[0]);
        _playManage.AddCell(8);
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