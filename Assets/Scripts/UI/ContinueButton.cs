using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : ButtonAction
{
    [SerializeField] EventsController _controller;

    public override void OnClickAction()
    {
        _controller.SetEvent(0, true);
    }
}
