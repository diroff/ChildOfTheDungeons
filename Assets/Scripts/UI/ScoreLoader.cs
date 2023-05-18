using TMPro;
using UnityEngine;

public class ScoreLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreField;
    [SerializeField] private HighscoreStorageService _scoreService;

    private void Start()
    {
        SaveData data = _scoreService.GetData();

        _scoreField.text = "1. " + data.NameValue + ": " + data.ScoreValue;
    }
}