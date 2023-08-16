using UnityEngine;
using UnityEngine.Events;

public class Player : Fighter
{
    [Header("Items")]
    [SerializeField] private ArmorSlots _armorSlots;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private Inventory _inventory;
    [Space]

    [SerializeField] private int _experienceToNextLevel = 10;
    [SerializeField] private Skills _skills;
    [Space]

    [SerializeField] private Yandex _yandex;

    private int _baseExperience;
    private int _currentExperience = 0;
    private int _currentScore = 0;
    private int _coins = 0;

    public Inventory Inventory => _inventory;
    public WeaponSlot WeaponSlot => _weaponSlot;
    public ArmorSlots ArmorSlots => _armorSlots;
    public Skills Skills => _skills;

    public int CurrentScore => _currentScore;
    public int Coins => _coins;

    public event UnityAction Leaved;
    public event UnityAction NotLeaved;
    public event UnityAction<float, float> HealthChanged;
    public event UnityAction<int> CoinsCountChanged;

    public UnityEvent<int, int> ExperienceChanged;
    public UnityEvent<int> ExperienceAdded;
    public UnityEvent<int> LevelChanged;

    protected override void Start()
    {
        _baseExperience = _experienceToNextLevel;
        base.Start();
        CalculateMaxHealth();
        CalculateTotalDamage();
        Armor = _armorSlots.CalculateArmor();
        HealthChanged(CurrentHealth, MaxHealth);
        ExperienceChanged?.Invoke(_currentExperience, _experienceToNextLevel);
        CoinsCountChanged?.Invoke(_coins);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        HealthChanged(CurrentHealth, MaxHealth);
    }

    public void Heal()
    {
        if (PotionChecker())
        {
            CurrentHealth = MaxHealth;

            _inventory.SpendPotion();
            HealthChanged(CurrentHealth, MaxHealth);
            FighterAnimator.SetTrigger("Heal");
        }
    }

    public bool PotionChecker()
    {
        return _inventory.IsEnoughPotion();
    }

    public void AddHeal()
    {
        _inventory.AddPotion();
    }

    public void AddExperience(int countExperience)
    {
        _currentExperience += countExperience;
        _currentScore += countExperience;
        ExperienceAdded?.Invoke(countExperience);
        ExperienceChanged?.Invoke(_currentExperience, _experienceToNextLevel);
        LevelUp();
    }

    public void AddCoins(int count)
    {
        _coins += count;

        CoinsCountChanged?.Invoke(_coins);
    }

    public void SpendCoins(int count)
    {
        if(_coins >= count)
            _coins -= count;

        CoinsCountChanged?.Invoke(_coins);
    }

    public override void CalculateMaxHealth()
    {
        MaxHealth = BaseMaxHealth + (_skills.Endurance.CurrentLevel * 2);
    }

    private bool IsEnoughExperience()
    {
        return _currentExperience >= _experienceToNextLevel;
    }

    public void LevelUp()
    {
        if (!IsEnoughExperience())
            return;

        Level++;

        int remainExperience = _currentExperience - _experienceToNextLevel;
        _currentExperience = remainExperience;
        _skills.AddSkillPoint(_skills.SkillCountForLevel);

        SetLevelStats();

#if UNITY_WEBGL
        _yandex.Rate();
        _yandex.ShowAdvertisement();
#endif
    }

    public override void SetLevel(int currentLevel)
    {
        base.SetLevel(currentLevel);
        _experienceToNextLevel = (int)((_baseExperience * Level) * (Mathf.Pow(2, Level)));
        SetLevelStats();
    }

    private void SetLevelStats()
    {
        _experienceToNextLevel = (int)((_baseExperience * Level) * (Mathf.Pow(2, Level)));

        if (Level == 1)
            _experienceToNextLevel = 10;

        CalculateMaxHealth();
        CalculateTotalDamage();
        HealthChanged(CurrentHealth, MaxHealth);
        FillHealth();
        ExperienceChanged?.Invoke(_currentExperience, _experienceToNextLevel);
        LevelChanged?.Invoke(Level);
    }

    private void FillHealth()
    {
        CurrentHealth = MaxHealth;
        HealthChanged(CurrentHealth, MaxHealth);
    }

    public void AddArmor(Armor armor)
    {
        _armorSlots.AddItem(armor);
        Armor = _armorSlots.CalculateArmor();
    }

    public void UseWeapon(Weapon weapon)
    {
        _weaponSlot.AddItem(weapon);
    }

    public override void Dead()
    {
        base.Dead();
    }

    public void TryToLeave(int change)
    {
        if (IsLeaved(change))
            Leave();
        else
            NotLeave();
    }

    public void Leave()
    {
        PlayLeaveAnimation();
        Leaved?.Invoke();
    }

    public void NotLeave()
    {
        FighterAnimator.SetTrigger("NotLeave");
        NotLeaved?.Invoke();
    }

    public void Move()
    {
        FighterAnimator.SetTrigger("Run"); 
    }

    public void PlayLeaveAnimation()
    {
        FighterAnimator.SetTrigger("Leave");
    }

    public float CalculateTotalDamage(float modificator = 1)
    {
        if (_weaponSlot.IsSomeWeapon())
            return (_skills.Power.CurrentLevel + _weaponSlot.Weapon.CalculateDamage()) * modificator;

        return _skills.Power.CurrentLevel * modificator;
    }

    public bool HasAdditionalChance(int additionalChance)
    {
        int totalChance = (Random.Range(0, 101) + additionalChance);
        return totalChance  >= 100;
    }

    public bool IsLeaved(int additionalChance)
    {
        int totalChance = (Random.Range(0, 101) + additionalChance);
        return totalChance >= 100;
    }

    public void UpdateParameters()
    {
        CalculateMaxHealth();
        CalculateTotalDamage();
        FillHealth();
        HealthChanged(CurrentHealth, MaxHealth);
    }
}