using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : Event
{
    public override void DoEventSteps()
    {
        base.DoEventSteps();
    }

    public override void EndEvent()
    {
        base.EndEvent();
        SetEnableEvent(false);
        SetPanelState(false);
    }
}