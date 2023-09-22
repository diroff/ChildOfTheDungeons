using System;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialData[] _messages;
    [SerializeField] private bool _showOneTime = true;

    private bool _viewed = false;

    public TutorialData[] Message => _messages;
    public bool ShowOneTime => _showOneTime;

    public bool Viewed => _viewed;

    public void ViewMessage()
    {
        _viewed = true;
    }
}

[Serializable]
public class TutorialData
{
    [TextArea]
    public string Message;
    public bool NeedPause = false; 

    [SerializeField] private UnityEvent OnStartMessage;
    [SerializeField] private UnityEvent OnEndMessage;

    public void DoStartAction()
    {
        if(OnStartMessage != null) 
            OnStartMessage?.Invoke();
    }

    public void DoEndAction()
    {
        if(OnEndMessage != null)
            OnEndMessage?.Invoke();
    }
}