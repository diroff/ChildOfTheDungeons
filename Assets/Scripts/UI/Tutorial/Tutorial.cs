using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [TextArea(1, 3)]
    [SerializeField] private string[] _message;
    [SerializeField] private bool _showOneTime = true;

    private bool _viewed = false;

    public string[] Message => _message;
    public bool ShowOneTime => _showOneTime;
    public bool Viewed => _viewed;

    public void ViewMessage()
    {
        _viewed = true;
    }
}