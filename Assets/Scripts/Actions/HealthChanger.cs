using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthChanger : MonoBehaviour
{
    [SerializeField] private Fighter _startTarget;
    [SerializeField] private Fighter _endTarget;
    [SerializeField] private UnityAction _heal;

    public void Attack()
    {
        _startTarget.DealDamage(_endTarget);
    }

    public void Heal()
    {
        _heal?.Invoke();
    }
}
