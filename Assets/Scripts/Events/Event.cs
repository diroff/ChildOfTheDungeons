using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Event : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Sprite _eventIcon;

    [SerializeField] protected Spawner Spawner;

    [SerializeField] private float _panelEnableCouldown = 1.0f;

    public Sprite EventIcon => _eventIcon;

    public event UnityAction<bool> Ended;

    public void SetPanelState(bool enabled)
    {
        _panel.SetActive(enabled);
    }

    public virtual void SetEnableEvent(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    private IEnumerator EnableEvent()
    {
        SetPanelState(false);
        yield return new WaitForSeconds(_panelEnableCouldown);
        SetPanelState(true);
    }

    public virtual void StartEvent()
    {
        SetEnableEvent(true);
        StartCoroutine(EnableEvent());
    }

    public virtual void EndEvent()
    {
        Ended?.Invoke(true);
        SetEnableEvent(false);
        SetPanelState(false);
    }
}