using UnityEngine;

public class DontTakeButton : ButtonAction
{
    [SerializeField] private FreeItem _freeItem;

    public override void OnClickAction()
    {
        _freeItem.LeaveItem();
    }
}