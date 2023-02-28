using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyParameters : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [Space]
    [SerializeField] private Slider _healthSlider;

    private Enemy _enemy;

    private void OnEnable()
    {
        _enemy = _spawner.GetEnemy();
        _enemy.HealthChanged.AddListener(HealthDisplay);
        _enemy.DamageChanged.AddListener(damage => { _damageText.text = string.Format("{0:f0}", damage); });
        _enemy.UpdateParameters();
    }

    private void HealthDisplay(float currentHealth, float maxHealth)
    {
        _healthText.text = string.Format("{0:f0}", currentHealth);
        _healthSlider.value = (float)currentHealth / maxHealth;
    }
}