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
        }
    }

    public bool PotionChecker()
    {
        bool isSomething = _potions > 0;
        return isSomething;
    }

    public override void Dead()
    {
        Debug.Log("Игрок умер. Игра окончена!");
    }
}