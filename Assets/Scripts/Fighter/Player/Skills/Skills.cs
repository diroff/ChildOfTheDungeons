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
    private Skill _currentSkill;

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

    public void UpgradeSkill()
    {
        if (_skillPointCount <= 0)
        {
            SkillPointOver?.Invoke(IsEnoughPoints());
            return;
        }

        if(_currentSkill.AddLevel(1))
            _skillPointCount -= 1;
        
        SkillPointCountChanged?.Invoke(_skillPointCount);
        SkillPointOver?.Invoke(IsEnoughPoints());
    }

    public void SetActiveSkill(Skill skill)
    {
        _currentSkill = skill;
    }

    private bool IsEnoughPoints()
    {
        return _skillPointCount > 0;
    }
}