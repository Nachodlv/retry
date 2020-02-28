using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public enum SceneToLoad
    {
        Menu = 0,
        Level = 1
    }

    [SerializeField] [Tooltip("Scene to be loaded")]
    private SceneToLoad sceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene((int) sceneToLoad);
    }

    public static void LoadScene(SceneToLoad scene)
    {
        SceneManager.LoadScene((int) scene);
    }
}
