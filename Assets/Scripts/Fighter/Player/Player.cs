using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    [SerializeField] private List<HealItem> _potions = new List<HealItem>();

    public void TryToHeal()
    {
        if (PotionChecker())
        {
            if (CurrentHealth >= MaxHealth / 2)
                CurrentHealth = MaxHealth;
            else
                CurrentHealth += MaxHealth / 2;

            _potions.RemoveAt(_potions.Count-1);

            Debug.Log($"Потрачена хилка. Теперь их {_potions.Count}");
        }
    }

    private bool PotionChecker()
    {
        bool isSomething = _potions.Count > 0;
        return isSomething;
    }

    public void AddHeal()
    {
        _potions.Add(new HealItem());
        Debug.Log($"Добавлена хилка. Теперь их {_potions.Count}");
    }

    public override void Dead()
    {
        base.Dead();
    }

    public void AddItem(Item additionalItem)
    {
        var typeItem = additionalItem.GetItemType();

        switch (typeItem)
        {
            case Item.TypeOfItems.heal:
                AddHeal();
                break;
        }
    }
}