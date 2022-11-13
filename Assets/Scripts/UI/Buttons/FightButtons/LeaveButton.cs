using UnityEngine;

public class LeaveButton : ButtonAction
{
    [SerializeField] private Player _player;

    public override void OnClickAction()
    {
        _player.TryToLeave();
    }
}