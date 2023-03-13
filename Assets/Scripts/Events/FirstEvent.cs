using System.Collections;
using UnityEngine;

public class FirstEvent : Event
{
    [SerializeField] private EventsController _eventsController;
    [SerializeField] private Player _player;
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private float _doorOpenTime;
    [SerializeField] private float _playerMoveTime;

    private void OnEnable()
    {
        _player.TeleportToStartPosition();
    }

    public void Open()
    {
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        SetPanelState(false);
        _doorAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(_doorOpenTime);
        _player.StartMovement();
        yield return new WaitForSeconds(_playerMoveTime);
        EndEvent();
    }
}