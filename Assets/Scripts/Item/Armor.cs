using UnityEngine;
using UnityEngine.Events;

public class Armor : Item
{
    [SerializeField] private int _health;
    [SerializeField] private int _protection;

    public event UnityAction<int> ProtectionChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        CalculateProtection();
    }

    protected override void UpdateParameters()
    {
        base.UpdateParameters();
        CalculateProtection();
    }

    public int CalculateProtection()
    {
        int protection = _protection + (Level / 2);
        ProtectionChanged?.Invoke(protection);
        return protection;
    }
}