using UnityEngine;

public class ContinueButton : ButtonAction
{
    [SerializeField] Continue _continueEvent;

    public override void OnClickAction()
    {
        _continueEvent.ContinueWay();
    }
}
