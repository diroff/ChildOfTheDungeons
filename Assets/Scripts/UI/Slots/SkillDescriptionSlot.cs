using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDescriptionSlot : Slot
{
    [SerializeField] private Skill _skill;

    private void OnEnable()
    {
        IsFilled = true;
    }

    public override void ShowDescription()
    {
        base.ShowDescription();
        InfoPanel.SetInfo(_skill.Description);
    }
}
