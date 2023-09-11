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

        /*SaveData data = new SaveData();

        data.ScoreValue = _progressionController.CurrentPoints;

        _highscoreStorageService.SaveScore(data);*/
        //_progressSaveLoader.Save();
    }

    public override void EndEvent()
    {
        base.EndEvent();
    }
}