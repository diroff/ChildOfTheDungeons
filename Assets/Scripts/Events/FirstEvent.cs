using System.Collections;
using UnityEngine;

public class FirstEvent : Event
{
    [Header("Events")]
    [SerializeField] private EventsController _eventsController;
    [SerializeField] private Fight _fightEvent;
    [Space]

    [SerializeField] private Enemy _nextEnemy;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private float _playerMoveTime;

    public void Open()
    {
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        SetPanelState(false);
        SpawnRoom();
        yield return new WaitForSeconds(_playerMoveTime);
        EndEvent();
    }

    private void SpawnRoom()
    {
        RoomController.SpawnRoom(RoomController.DefaultRoom);
        RoomController.MoveBackground();
    }

    public override void EndEvent()
    {
        _eventsController.SetNextEvent(_fightEvent);
        _fightEvent.SetEnemy(_nextEnemy);
        base.EndEvent();
    }
}