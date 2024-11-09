using UnityEngine;

namespace Metronome.timbre
{
    [CreateAssetMenu(menuName = "Timbre/Timbre_SO")]
    public class Timbre_SO:ScriptableObject
    {
        /// <summary>
        /// 音频文件
        /// </summary>
        [SerializeField]
        private AudioClip audioClip;
        public AudioClip AudioClip => audioClip;
        
        /// <summary>
        /// 名字
        /// </summary>
        [SerializeField]
        private string audioFileName;
        public string AudioFileName => audioFileName;
    }
}