using UnityEngine;

public class ScoreLoader : MonoBehaviour
{
    [SerializeField] private Score _scorePrefab;
    [SerializeField] private GameObject _scorePlacement;
    [SerializeField] private HighscoreStorageService _scoreService;

    private void Start()
    {
        Scores scores = _scoreService.GetData();

        for (int i = 0; i < _scoreService.SavesCount; i++)
        {
            var scoreField = Instantiate(_scorePrefab, _scorePlacement.transform);
            var score = scores.Saves[i];
            scoreField.SetScore($"{i + 1}. {score.ScoreValue}");

            if (score.ScoreValue == 0)
                scoreField.HideRecord();
        }
    }
}