using System;
using UnityEngine;
using UnityEngine.Events;

public class Skills : MonoBehaviour
{
    [SerializeField] private Skill _power;
    [SerializeField] private Skill _agility;
    [SerializeField] private Skill _luck;
    [SerializeField] private Skill _endurance;

    [Space]
    [SerializeField] private int _skillCountForLevel = 1;

    private int _skillPointCount = 0;

    public UnityEvent<int> SkillPointCountChanged;
    public UnityEvent<bool> SkillPointOver;
    public int SkillCountForLevel => _skillCountForLevel;

    public Skill Power => _power;
    public Skill Agility => _agility;
    public Skill Luck => _luck;
    public Skill Endurance => _endurance;

    private void OnEnable()
    {
        SkillPointCountChanged?.Invoke(_skillPointCount);
    }

    public void AddSkillPoint(int count)
    {
        _skillPointCount += count;
        SkillPointCountChanged?.Invoke(_skillPointCount);
    }

    public void UpgradeSkill(Skill skill)
    {
        if (_skillPointCount <= 0)
        {
            SkillPointOver?.Invoke(IsEnoughPoints());
            return;
        }

        _skillPointCount -= 1;
        skill.AddLevel(1);
        
        SkillPointCountChanged?.Invoke(_skillPointCount);
        
        SkillPointOver?.Invoke(IsEnoughPoints());
    }

    private bool IsEnoughPoints()
    {
        return _skillPointCount > 0;
    }
}