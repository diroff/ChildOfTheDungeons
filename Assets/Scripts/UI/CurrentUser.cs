using TMPro;
using UnityEngine;

public class CurrentUser : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private CurrentUserStorageService _storageService;

    private void Start()
    {
        _inputField.text = _storageService.GetData().Name;
    }

    public void SaveName()
    {
        _storageService.Save(_inputField.text);
    }
}