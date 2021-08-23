using UnityEngine.SceneManagement;


public class SceneChangeEditor : MonoSingleton<SceneChangeEditor>
{
    public void NextLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
