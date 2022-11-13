using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Fighter
{
    [SerializeField] private List<HealItem> _potions = new List<HealItem>();

    [SerializeField] private Helm _helmPlace;
    [SerializeField] private Breastplate _breastplate;
    [SerializeField] private Pants _pants;
    [SerializeField] private Shoes _shoes;

    public event UnityAction Leaved;

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

    public void TryToLeave()
    {
        Leave();
    }

    public void Leave()
    {
        Leaved?.Invoke();
    }
}