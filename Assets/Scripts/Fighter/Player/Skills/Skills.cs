using UnityEngine;
using UnityEngine.Events;

public class Skills : MonoBehaviour
{
    [SerializeField] private Skill _power;
    [SerializeField] private Skill _agility;
    [SerializeField] private Skill _luck;
    [SerializeField] private Skill _endurance;

    private int _skillPointCount = 0;

    public UnityEvent<int> SkillPointCountChanged;

    public Skill Power => _power;
    public Skill Agility => _agility;
    public Skill Luck => _luck;
    public Skill Endurance => _endurance;

    private void Start()
    {
        SkillPointCountChanged?.Invoke(_skillPointCount);
    }

    public void AddSkillPoint(int count)
    {
        _skillPointCount += count;
        SkillPointCountChanged?.Invoke(_skillPointCount);
    }

    public void UpgradeSkill(Skill skill, int levelCount)
    {
        _skillPointCount -= levelCount;
        skill.AddLevel(levelCount);
        SkillPointCountChanged?.Invoke(_skillPointCount);
    }
}