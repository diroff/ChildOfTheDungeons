using UnityEngine;

public class Armor : Item
{
    [SerializeField] private int _health;
    [SerializeField] private int _protection;

    public int CalculateProtection()
    {
        return _protection + (Level / 2);
    }
}