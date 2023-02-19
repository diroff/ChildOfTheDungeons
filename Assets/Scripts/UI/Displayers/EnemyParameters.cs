using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyParameters : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _levelText;

    private Enemy _enemy;

    private void OnEnable()
    {
        _enemy = _spawner.GetEnemy();
        _enemy.HealthChanged.AddListener(health => { _healthText.text = $"x{health}"; });
        _enemy.DamageChanged.AddListener(damage => { _damageText.text = $"x{damage}"; });
        _enemy.LevelChanged.AddListener(level => { _levelText.text = $"x{level}"; });
        _enemy.UpdateParameters();
    }
}