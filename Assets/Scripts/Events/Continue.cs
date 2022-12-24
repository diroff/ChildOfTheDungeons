using System.Collections;
using UnityEngine;

public class Continue : Event
{
    [SerializeField] private EventsController _controller;
    [SerializeField] private Player _player;

    [SerializeField] private float _runCouldown = 2.0f;

    public override void StartEvent()
    {
        base.StartEvent();
    }

    public void ContinueWay()
    {
        StartCoroutine(ContinueCoroutine());
    }

    private IEnumerator ContinueCoroutine()
    {
        SetPanelState(false);
        _player.Run();
        yield return new WaitForSeconds(_runCouldown);
        _controller.CurrentEvent.EndEvent();
        _controller.SetEvent(0, true);
        _controller.StartEvent();
    }

    public override void EndEvent()
    {
        base.EndEvent();
        SetEnableEvent(false);
        SetPanelState(false);
    }
}