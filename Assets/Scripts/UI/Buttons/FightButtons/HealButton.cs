using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealButton : ButtonAction
{
    [SerializeField] private Player _player;

    public override void OnClickAction()
    {
        _player.Heal();
    }
}