using UnityEngine;

public class GameOver : Event
{
    [SerializeField] private ProgressionController _progressionController;
    [SerializeField] private HighscoreStorageService _highscoreStorageService;
    [SerializeField] private CurrentUserStorageService _currentUserStorageService;
    [SerializeField] private ProgressSaveLoader _progressSaveLoader;

    public override void StartEvent()
    {
        if (gameObject.activeSelf)
            return;
        
        base.StartEvent();

        /*SaveData data = new SaveData();

        data.NameValue = _currentUserStorageService.GetData().Name;
        data.ScoreValue = _progressionController.CurrentPoints;

        _highscoreStorageService.SaveScore(data);
        _progressSaveLoader.Save();*/
    }

    public override void EndEvent()
    {
        base.EndEvent();
    }
}