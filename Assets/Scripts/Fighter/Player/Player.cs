using UnityEngine;

public class Player : Fighter
{
    [SerializeField] private int _potions;

    public void TryToHeal()
    {
        if (PotionChecker())
        {
            if (CurrentHealth >= MaxHealth / 2)
                CurrentHealth = MaxHealth;
            else
                CurrentHealth += MaxHealth / 2;

            _potions--;

            Debug.Log($"Потрачена хилка. Теперь их {_potions}");
        }
    }

    private bool PotionChecker()
    {
        bool isSomething = _potions > 0;
        return isSomething;
    }

    public void AddHeal(int count)
    {
        _potions += count;
        Debug.Log($"Добавлено {count} хилок. Теперь их {_potions}");
    }

    public override void Dead()
    {
        base.Dead();
    }
}