using TMPro;
using UnityEngine;

public class PlayerParametersDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _damageField;
    [SerializeField] private TextMeshProUGUI _potionField;

    private void OnEnable()
    {
        _player.DamageChanged += DisplayPlayerDamage;
        _player.PotionCountChanged += DisplayPlayerPotionCount;
    }

    private void OnDisable()
    {
        _player.DamageChanged -= DisplayPlayerDamage;
        _player.PotionCountChanged -= DisplayPlayerPotionCount;
    }

    private void DisplayPlayerDamage(int damage)
    {
        _damageField.text = "x" + damage;
    }

    private void DisplayPlayerPotionCount(int potion)
    {
        _potionField.text = "x" + potion;
    }
}
