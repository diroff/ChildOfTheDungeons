using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : Event
{
    public override void DoEventSteps()
    {
        SetPanel(true);
    }

    public override void EndEvent()
    {
        SetPanel(false);
        base.EndEvent();
    }
}
