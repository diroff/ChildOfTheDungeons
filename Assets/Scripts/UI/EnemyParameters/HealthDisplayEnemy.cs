using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplayEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TextMeshProUGUI _healthText;

    private void OnEnable()
    {
        _enemy.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _enemy.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        _healthText.text = "x" + health;
    }
}
