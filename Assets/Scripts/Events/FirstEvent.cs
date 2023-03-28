using System.Collections;
using UnityEngine;

public class FirstEvent : Event
{
    [SerializeField] private EventsController _eventsController;
    [SerializeField] private Fight _fightEvent;
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
        RoomController.SpawnRoom(RoomController.DefaultRoom);
        Player.Run();
        RoomController.MoveBackground();
        yield return new WaitForSeconds(_playerMoveTime);
        _eventsController.SetNextEvent(_fightEvent);
        _fightEvent.SetEnemy(_nextEnemy);
        EndEvent();
    }
}