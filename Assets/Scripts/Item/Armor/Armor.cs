using UnityEngine;

public abstract class Armor : Item
{
    [SerializeField] private int _health;
    [SerializeField] private int _protection;
    [SerializeField] private int _level;
    [SerializeField] private Sprite _sprite;

    public int CalculateProtection()
    {
        return _protection * (_level / 2);
    }
}