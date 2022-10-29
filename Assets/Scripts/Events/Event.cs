using UnityEngine;
using UnityEngine.Events;

public abstract class Event : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
        
    }

    public event UnityAction<bool> Ended;

    public void SetPanel(bool isSpawn)
    {
        if (isSpawn)
        {
            GameObject panel = Instantiate(_panel);
        }
        else
            Destroy(_panel.gameObject);
    }

    public abstract void DoEventSteps();

    public virtual void EndEvent()
    {
        Ended?.Invoke(true);
    }
}