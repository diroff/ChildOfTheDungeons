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
    [SerializeField] private HighscoreStorageService _highscoreStorageService;
    [SerializeField] private ProgressSaveLoader _progressSaveLoader;
    [SerializeField] private LevelCompleteCount _levelCompleteCount;
    [SerializeField] private Yandex _yandex;

    public void FinishGame()
    {
        StartCoroutine(Exit());
    }

    private IEnumerator Exit()
    {
        _progressSaveLoader.Save();
        _levelCompleteCount.SaveCompleteLevel();
        _blackScreen.gameObject.SetActive(true);
        SetPanelState(false);
        Player.Move();

#if UNITY_WEBGL && !UNITY_EDITOR
            _yandex.SetLeaderboard(_levelCompleteCount.GetData().Count);
#endif
        Debug.Log("Leaderboard was setted with value " + _levelCompleteCount.GetData().Count);
        yield return new WaitForSeconds(_walkAnimationTime);
        _blackScreen.SetTrigger("On");
        yield return new WaitForSeconds(_endGameTime);

        _levelOverPanel.gameObject.SetActive(true);
    }
}