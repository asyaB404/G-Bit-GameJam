using System.Collections.Generic;
using Metronome;
using Metronome.timbre;
using UnityEngine;
using UnityEngine.UI;

public class UIManage:IMetronomUI
{
   //UI面板
   public GameObject Main;
   //音色队列注册表
   private Dictionary<ITimbre, GameObject> _timbres = new Dictionary<ITimbre, GameObject>();

   public UIManage()
   {
      Init();
   }
   public void Init()
   {
      Main =  GameObject.Instantiate(AssetMgr.LoadAssetSync<GameObject>("Assets/AddressableAssets/Prefabs/Canvas.prefab")).transform.GetChild(0).gameObject;
   }

   public void AddTimbre(ITimbre timbre)
   {
      if (_timbres.ContainsKey(timbre))
      {
         Debug.LogError("UI模块中已经注册该音色");
         return;
      }
      var queue =  GameObject.Instantiate(AssetMgr.LoadAssetSync<GameObject>("Assets/AddressableAssets/Prefabs/Queue.prefab"), Main.transform);
      _timbres.Add(timbre,queue);
   }

   public void DeleteTimbre(ITimbre timbre)
   {
      if (!_timbres.ContainsKey(timbre))
      {
         Debug.LogError("UI模块中已经不包含该音色");
         return;
      }
      Object.Destroy(_timbres[timbre]);
      _timbres.Remove(timbre);
   }

   public void AddCell(ITimbre timbre,MCell cell)
   {
      
         var cellg  = GameObject.Instantiate(AssetMgr.LoadAssetSync<GameObject>("Assets/AddressableAssets/Prefabs/Cell.prefab"),_timbres[timbre].transform);
         var buttoncell = cellg.GetComponent<Button>();
         buttoncell.onClick.AddListener(() =>
         {
            cell.ChangePlayState();
            if (cell.Canplay)
            {
               buttoncell.image.color = Color.green;
            }
            else
            {
               buttoncell.image.color = Color.white;
            }
         });
      
   }

   public void RemoveCell(ITimbre timbre,MCell cell)
   {
      var button = _timbres[timbre].transform;
      button.GetChild(button.childCount-1).GetComponent<Button>().onClick.RemoveAllListeners();
      Object.Destroy(button.GetChild(button.childCount-1).gameObject);
   }
   

   public void ReplaceTimbre(ITimbre beforetimbre, ITimbre newtimbre)
   {
      var queue = _timbres[beforetimbre];
      _timbres.Remove(beforetimbre);
      _timbres.Add(newtimbre,queue);
      Debug.LogWarning(beforetimbre+"替换"+newtimbre);
      
      
   }

   
}
