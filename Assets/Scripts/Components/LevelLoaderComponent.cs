using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderComponent : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}