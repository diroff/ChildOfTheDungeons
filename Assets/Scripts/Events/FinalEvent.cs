using System.Collections;
using UnityEngine;

public class FinalEvent : Event
{
    [Header("Coroutine time")]
    [SerializeField] private float _walkAnimationTime;
    [SerializeField] private float _endGameTime;
    [Space]

    [SerializeField] private Animator _blackScreen;

    [SerializeField] private LevelOverPanel _levelOverPanel;

    [Header("Services")]
    [SerializeField] private ProgressionController _progressionController;
    [SerializeField] private HighscoreStorageService _highscoreStorageService;
    [SerializeField] private CurrentUserStorageService _currentUserStorageService;
    //[SerializeField] private ProgressSaveLoader _progressSaveLoader;

    public void FinishGame()
    {
        StartCoroutine(Exit());
    }

    private IEnumerator Exit()
    {
        //_progressSaveLoader.Save();
        _blackScreen.gameObject.SetActive(true);
        SetPanelState(false);
        Player.Move();
        yield return new WaitForSeconds(_walkAnimationTime);
        _blackScreen.SetTrigger("On");
        yield return new WaitForSeconds(_endGameTime);

        //_levelOverPanel.gameObject.SetActive(true);
        
        SaveData data = new SaveData();
        data.ScoreValue = _progressionController.CurrentPoints;

        _highscoreStorageService.SaveScore(data);
    }
}