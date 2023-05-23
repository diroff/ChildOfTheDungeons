using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyParameters : MonoBehaviour
{
    [SerializeField] private RoomController _roomController;
    [SerializeField] private Slider _healthSlider;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _damageText;

    private Spawner _spawner;
    private Enemy _enemy;

    private void OnEnable()
    {
        _spawner = _roomController.GetRoomSpawner();
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