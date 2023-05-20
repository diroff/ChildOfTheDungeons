using UnityEngine;

public class HighscoreStorageService : MonoBehaviour
{
    public const string Key = "Score";

    public SaveData CurrentData;

    private IStorageService _storageService;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<SaveData>(Key, data =>
        {
            if (data == default)
                FirstSave();
        });
    }

    public void SaveScore(SaveData saveData)
    {
        bool canBeSaved = true;

        _storageService.Load<SaveData>(Key, data =>
        {
            if (saveData.ScoreValue <= data.ScoreValue)
                canBeSaved = false;
        });

        if (canBeSaved)
            _storageService.Save(Key, saveData);
    }

    private void FirstSave()
    {
        SaveData saveData = new SaveData();

        saveData.NameValue = "Empty";
        saveData.ScoreValue = 0;

        _storageService.Save(Key, saveData);
    }

    public void LoadScore()
    {
        _storageService.Load<SaveData>(Key, data => { CurrentData = data; });
    }

    public SaveData GetData()
    {
        LoadScore();
        return CurrentData;
    }
}

public class SaveData
{
    public int ScoreValue { get; set; }
    public string NameValue { get; set; }
}
