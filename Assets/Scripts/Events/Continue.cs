using System.Collections;
using UnityEngine;

public class Continue : Event
{
    [SerializeField] private EventsController _controller;

    [SerializeField] private float _runCouldown = 1.5f;

    public override void StartEvent()
    {
        SetEnableEvent(true);
        SetPanelState(true);
    }

    public void ContinueWay()
    {
        StartCoroutine(ContinueCoroutine());
    }

    private IEnumerator ContinueCoroutine()
    {
        SetPanelState(false);
        EnableNewEvent();
        RoomController.MoveBackground();
        yield return new WaitForSeconds(_runCouldown);
    }

    public override void EndEvent()
    {
        base.EndEvent();
        SetEnableEvent(false);
        SetPanelState(false);
    }

    private void EnableNewEvent()
    {
        _controller.CurrentEvent.EndEvent();
        _controller.SetEvent(0, true);
        RoomController.SpawnRoom(_controller.CurrentEvent.Room);
        _controller.StartEvent();
    }
}