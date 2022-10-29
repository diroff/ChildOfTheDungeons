using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LootAction : MonoBehaviour
{
    [SerializeField] private UnityAction _loot;

    public void Loot()
    {
        _loot?.Invoke();
    }
}