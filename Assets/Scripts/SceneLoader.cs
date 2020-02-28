using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <para>Loads a specific scene</para>
/// </summary>
public class SceneLoader: MonoBehaviour
{
    public enum SceneToLoad
    {
        Menu = 0,
        Level = 1
    }

    [SerializeField] [Tooltip("Scene to be loaded")]
    private SceneToLoad sceneToLoad;

    /// <summary>
    /// <para>Loads the scene specified in the sceneToLoad</para>
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene((int) sceneToLoad);
    }

    public static void LoadScene(SceneToLoad scene)
    {
        SceneManager.LoadScene((int) scene);
    }
}
