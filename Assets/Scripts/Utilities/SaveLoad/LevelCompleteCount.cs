using System;
using UnityEngine;

public class LevelCompleteCount : MonoBehaviour
{
    public const string Key = "Levels";

    private LevelsComplete _levelsComplete;
    private IStorageService _storageService;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        CheckData();
    }

    public void SaveCompleteLevel()
    {
        CheckData();

        LevelsComplete scores = new LevelsComplete();

        _storageService.Load<LevelsComplete>(Key, data =>
        {
            scores = data;
            scores.Count++;
        });

        _storageService.Save(Key, scores);
    }

    private void CheckData()
    {
        _storageService.Load<LevelsComplete>(Key, data =>
        {
            if (data == default)
                FirstSave();
        });
    }

    private void FirstSave()
    {
        LevelsComplete scores = new LevelsComplete();
        scores.Count = 0;

        _storageService.Save(Key, scores);
    }

    public void LoadScore()
    {
        _storageService.Load<LevelsComplete>(Key, data => { _levelsComplete = data;});
    }

    public LevelsComplete GetData()
    {
        LoadScore();
        return _levelsComplete;
    }
}

public class LevelsComplete
{
    public int Count;
}