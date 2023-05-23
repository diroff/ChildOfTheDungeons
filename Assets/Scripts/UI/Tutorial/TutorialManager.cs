using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private TextMeshProUGUI _textField;

    private Queue<string> _currentMessages = new Queue<string>();

    public void AddMessages(Tutorial tutorial)
    {
        if (tutorial.Message.Length == 0 || tutorial.Viewed)
            return;

        _currentMessages.Clear();
        _tutorialPanel.SetActive(true);

        for (int i = 0; i < tutorial.Message.Length; i++)
        {
            _currentMessages.Enqueue(tutorial.Message[i]);
        }

        if (tutorial.ShowOneTime)
            tutorial.ViewMessage();

        ShowMessage();
    }

    public void ShowMessage()
    {
        if (_currentMessages.Count <= 0)
        {
            HideMessage();
            return;
        }

        _textField.text = _currentMessages.Dequeue();
    }

    public void HideMessage()
    {
        _currentMessages.Clear();
        _tutorialPanel.SetActive(false);
    }
}