using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangeEditor : MonoSingleton<SceneChangeEditor>
{
    private void Start()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
    }
    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex+1 >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
