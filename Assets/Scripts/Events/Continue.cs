using System.Collections;
using UnityEngine;

public class Continue : Event
{
    [SerializeField] private EventsController _controller;

    [SerializeField] private float _runCouldown = 1.5f;
    [SerializeField] private float _destroyCouldown = 0.5f;

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
        PlayerRun();
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

    private void PlayerRun()
    {
        SetPanelState(false);
        Player.Run();
    }
}