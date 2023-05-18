using UnityEngine;

public class HighscoreStorageService : MonoBehaviour
{
    public const string Key = "Score";

    public SaveData CurrentData;

    private IStorageService _storageService;

    private void OnEnable()
    {
        _storageService = new JsonToFileStorageService();
    }

    public void SaveScore(SaveData saveData)
    {
        _storageService.Save(Key, saveData);
        Debug.Log("Saved!");
    }

    public void LoadScore()
    {
        _storageService.Load<SaveData>(Key, data =>
        {
            CurrentData = data;
            Debug.Log($"Loaded. Name value: {CurrentData.NameValue} + Score value: {CurrentData.ScoreValue}");
        });
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
