using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighscoreStorageService : MonoBehaviour
{
    public const string Key = "Score";

    public int SavesCount { get; private set; } = 5;

    private Scores _scores;
    private IStorageService _storageService;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<Scores>(Key, data =>
        {
            if (data == default)
                FirstSave();
        });
    }

    public void SaveScore(SaveData saveData)
    {
        _storageService.Load<Scores>(Key, data =>
        {
            if (data == default)
                FirstSave();
        });

        Scores scores = new Scores();

        _storageService.Load<Scores>(Key, data =>
        {
            data.Saves.Add(saveData);
            var newData = data.Saves.OrderByDescending(m => m.ScoreValue).ToList();
            data.Saves = newData;
            data.Saves.RemoveAt(SavesCount);
            scores = data;
        });

        _storageService.Save(Key, scores);
    }

    private void FirstSave()
    {
        Scores scores = new Scores();
        scores.Saves = new List<SaveData>();

        SaveData saveData = new SaveData();

        saveData.NameValue = "Empty";
        saveData.ScoreValue = 0;

        for (int i = 0; i < SavesCount; i++)
        {
            scores.Saves.Add(saveData);
        }

        _storageService.Save(Key, scores);
    }

    public void LoadScore()
    {
        _storageService.Load<Scores>(Key, data => { _scores = data; });
    }

    public Scores GetData()
    {
        LoadScore();
        return _scores;
    }
}

public class SaveData
{
    public int ScoreValue { get; set; }
    public string NameValue { get; set; }
}

public class Scores
{
    public List<SaveData> Saves;
}