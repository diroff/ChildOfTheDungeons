using System.Collections;
using UnityEngine;

public class AttackButton : ButtonAction
{
    [SerializeField] private Fight _fight;

    public override void OnClickAction()
    {
        _fight.AttackEnemy();
    }
}