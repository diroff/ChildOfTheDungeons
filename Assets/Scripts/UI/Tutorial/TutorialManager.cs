using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _backgroundBlockPanel;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private TextMeshProUGUI _textField;

    private Queue<TutorialData> _currentMessages = new Queue<TutorialData>();
    private TutorialData _currentTutorialMessage;

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
        _backgroundBlockPanel.SetActive(true);
    }

    public void ShowMessage()
    {
        if (_currentTutorialMessage != null)
        {
            _currentTutorialMessage.DoEndAction();

            if (_currentTutorialMessage.NeedPause)
                _timeController.StopTime();
        }

        if (_currentMessages.Count <= 0)
        {
            _currentTutorialMessage = null;
            HideMessage();
            return;
        }

        _currentTutorialMessage = _currentMessages.Dequeue();
        _textField.text = _currentTutorialMessage.Message;
        _currentTutorialMessage.DoStartAction();
    }

    public void HideMessage()
    {
        _currentMessages.Clear();
        _backgroundBlockPanel.SetActive(false);
        _tutorialPanel.SetActive(false);
        _timeController.StartTime();
    }
}