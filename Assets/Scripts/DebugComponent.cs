using UnityEngine;

public class DebugComponent : MonoBehaviour
{
    [SerializeField] private string _debugMessage;

    public void ShowDebugMessage()
    {
        Debug.Log(_debugMessage);
    }
}