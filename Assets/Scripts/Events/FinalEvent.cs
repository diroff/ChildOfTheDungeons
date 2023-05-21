using System.Collections;
using UnityEngine;

public class FinalEvent : Event
{
    [SerializeField] private float _walkAnimationTime;
    [SerializeField] private float _endGameTime;
    [SerializeField] private Animator _blackScreen;

    [SerializeField] private ProgressionController _progressionController;
    [SerializeField] private HighscoreStorageService _highscoreStorageService;
    [SerializeField] private CurrentUserStorageService _currentUserStorageService;
    [SerializeField] private MenuControl _menuControl;

    public void FinishGame()
    {
        StartCoroutine(Exit());
    }

    private IEnumerator Exit()
    {
        _blackScreen.gameObject.SetActive(true);
        SetPanelState(false);
        Player.Move();
        yield return new WaitForSeconds(_walkAnimationTime);
        _blackScreen.SetTrigger("On");
        yield return new WaitForSeconds(_endGameTime);

        SaveData data = new SaveData();

        data.NameValue = _currentUserStorageService.GetData().Name;
        data.ScoreValue = _progressionController.CurrentPoints;

        _highscoreStorageService.SaveScore(data);
        _menuControl.LoadLevel();
    }
}