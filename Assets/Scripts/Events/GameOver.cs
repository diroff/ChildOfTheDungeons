using UnityEngine;

public class GameOver : Event
{
    [SerializeField] private Yandex _yandex;
    [SerializeField] private ProgressionController _progressionController;
    [SerializeField] private HighscoreStorageService _highscoreStorageService;
    [SerializeField] private ProgressSaveLoader _progressSaveLoader;

    public override void StartEvent()
    {
        if (gameObject.activeSelf)
            return;
        
        base.StartEvent();
    }

    public override void EndEvent()
    {
        base.EndEvent();
    }
}