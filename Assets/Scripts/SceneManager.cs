public static class SceneManager
{
    public static void NextLevel()
    {
        AudioManager.Instance.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().buildIndex + 1);
    }

    public static void End()
    {
        AudioManager.Instance.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

    public static void Reset()
    {
        AudioManager.Instance.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().buildIndex);
    }
}