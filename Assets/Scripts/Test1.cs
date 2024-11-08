using UnityEngine;


public class Test1 : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    [ContextMenu(nameof(TestAudioManager))]
    private void TestAudioManager()
    {
        AudioManager.Instance.PlaySound(audioClip);
    }
}