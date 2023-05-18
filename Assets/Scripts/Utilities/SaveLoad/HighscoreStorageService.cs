using UnityEngine;

public class HighscoreStorageService : MonoBehaviour
{
    private const string _key = "Score";
    private IStorageService _storageService;

    private void Start()
    {
        _storageService = new JsonToFileStorageService();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveData data = new SaveData();

            data.ScoreValue = 15;
            data.NameValue = "Porosb";

            _storageService.Save(_key, data);
            Debug.Log("Saved!");
        }

        if(Input.GetKey(KeyCode.Return))
        {
            _storageService.Load<SaveData>(_key, data =>
            {
                Debug.Log($"Loaded. Name value: {data.NameValue} +. Score value: {data.ScoreValue}");
            });
        }
    }
}

public class SaveData
{
    public int ScoreValue { get; set; }
    public string NameValue { get; set; }
}
