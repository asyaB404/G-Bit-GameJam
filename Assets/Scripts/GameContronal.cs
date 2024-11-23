using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameTools.MonoTool;
using GameTools.MonoTool.Player;
using Metronome.timbre;
using UnityEngine;

public class GameContronal : MonoBehaviour
{
    private PlayManage _playManage;
    public PlayManage PlayManage => _playManage;

    private GameObject _player;
    public GameObject Player => _player;

    [SerializeField,Header("BPM数值")]
    private int BPM;

    [Header("音色的列表")]
    public List<Timbre_SO> audioChilps;
    
    //音色实例
    private List<Timbre_Common> _timbre = new List<Timbre_Common>();

    private List<Platform> Platform;
    void Awake()
    {
        //控制器
        _playManage = new PlayManage();
        _player = GameObject.FindObjectOfType<PlayerContronal>().gameObject;

        //实例化音色
        foreach (var V in audioChilps)
        {
            _timbre.Add(new Timbre_Common(V));
        }
        
        //找到场景中工具(平台)
        Platform = GameObject.FindObjectsOfType<Platform>().ToList();
        Debug.Log(Platform.Count);
        
    }

    private void Start()
    {
        _timbre[0].EventManager.AddListener(TimbreEvent.AfterHit,(() =>
        {
            foreach (var p in Platform)
            {
                if (p.transform.position.y < 0)
                {
                    p.transform.position = new Vector3(p.transform.position.x, 5, p.transform.position.z);
                }
                else
                {
                    p.transform.position = new Vector3(p.transform.position.x, -1, p.transform.position.z);
                }
            }
        }));
        
        _playManage.
        
        _playManage.AddTimbre(_timbre[0]);
        _playManage.AddCell(8);
        _playManage.Play(BPM);
    }
}
