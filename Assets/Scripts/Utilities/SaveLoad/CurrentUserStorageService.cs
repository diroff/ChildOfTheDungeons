using UnityEngine;

public class CurrentUserStorageService : MonoBehaviour
{
    public const string Key = "User";

    private IStorageService _storageService;
    private User _currentUser;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<User>(Key, data =>
        {
            if (data == default)
                Save("Empty");
        });
    }

    public void Save(string name)
    {
        User user = new User();
        user.Name = name;

        _storageService.Save(Key, user);
    }

    public void Load()
    {
        _storageService.Load<User>(Key, data => { _currentUser = data; });
    }

    public User GetData()
    {
        Load();
        return _currentUser;
    }
}

public class User
{
    public string Name;
}