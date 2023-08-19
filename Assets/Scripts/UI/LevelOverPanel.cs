using TMPro;
using UnityEngine;

public class LevelOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelTextField;
    [SerializeField] private CurrentLevelProgression _currentLevelProgression;
    [SerializeField] private LevelLoaderComponent _levelLoaderComponent;

    private string _nextLevelName;

    /*private void OnEnable()
    {
        _levelTextField.text = "Уровень пройден!";
        _nextLevelName = _levelLoaderComponent.SceneName;
        _currentLevelProgression.Save(_nextLevelName);
    }*/

    public void OpenNextLevel()
    {
        _levelLoaderComponent.LoadLevel(_nextLevelName);
    }
}