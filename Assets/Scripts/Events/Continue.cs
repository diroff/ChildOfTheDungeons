using System.Collections;
using UnityEngine;

public class Continue : Event
{
    [SerializeField] private EventsController _controller;
    [SerializeField] private Player _player;

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
        yield return new WaitForSeconds(_runCouldown);

        DestroyEnemy();
        yield return new WaitForSeconds(_destroyCouldown);
        EnableNewEvent();
    }

    public override void EndEvent()
    {
        base.EndEvent();
        SetEnableEvent(false);
        SetPanelState(false);
    }

    private void DestroyEnemy()
    {
        var enemy = Spawner.GetComponentInChildren<Enemy>();
        if (enemy != null)
            Destroy(enemy.gameObject);
    }

    private void EnableNewEvent()
    {
        _controller.CurrentEvent.EndEvent();
        _controller.SetEvent(0, true);
        _controller.StartEvent();
    }

    private void PlayerRun()
    {
        SetPanelState(false);
        _player.Run();
    }
}