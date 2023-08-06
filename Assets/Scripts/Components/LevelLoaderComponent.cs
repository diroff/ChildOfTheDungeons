using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderComponent : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    [SerializeField] private CurrentLevelProgression _levelProgression;
    [SerializeField] private string _defaultLevelName;

    public string SceneName => _sceneName;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void LoadLastLevel()
    {
        LevelProgression data = _levelProgression.GetData();

        if (data != null)
            LoadLevel(data.LevelName);
        else
            LoadLevel(_defaultLevelName);
    }
}