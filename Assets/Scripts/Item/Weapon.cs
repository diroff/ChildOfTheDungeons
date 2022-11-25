using UnityEngine;

public class Weapon : Item
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    public int CalculateDamage()
    {
        return _damage + (Level / 2);
    }
}