using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageDisplayEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TextMeshProUGUI _damageText;

    private void OnEnable()
    {
        _enemy.DamageChanged += OnDamageChanged;
    }

    private void OnDisable()
    {
        _enemy.DamageChanged -= OnDamageChanged;
    }

    private void OnDamageChanged(int damage)
    {
        _damageText.text = "x" + damage;
    }
}
