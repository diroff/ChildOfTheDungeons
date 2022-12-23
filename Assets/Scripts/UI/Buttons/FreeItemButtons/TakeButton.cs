using UnityEngine;
using static UnityEditor.Progress;

public class TakeButton : ButtonAction
{
    [SerializeField] private FreeItem _freeItem;

    public override void OnClickAction()
    {
        _freeItem.AddItem();
    }

}