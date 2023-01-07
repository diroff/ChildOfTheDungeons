using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Event : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] protected Spawner Spawner;

    [SerializeField] private float _panelEnableCouldown = 1.0f;

    public event UnityAction<bool> Ended;

    public void SetPanelState(bool enabled)
    {
        _panel.SetActive(enabled);
    }

    public virtual void SetEnableEvent(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    public virtual void StartEvent()
    {
        SetEnableEvent(true);
        StartCoroutine(EnableEvent());
    }

    private IEnumerator EnableEvent()
    {
        SetPanelState(false);
        yield return new WaitForSeconds(_panelEnableCouldown);
        SetPanelState(true);
    }

    public virtual void EndEvent()
    {
        Ended?.Invoke(true);
        SetEnableEvent(false);
        SetPanelState(false);
    }
}