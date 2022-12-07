using TMPro;
using UnityEngine;

public class PlayerParametersDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _damageField;
    [SerializeField] private TextMeshProUGUI _potionField;
    [SerializeField] private TextMeshProUGUI _experienceField;
    [SerializeField] private TextMeshProUGUI _levelField;

    private void OnEnable()
    {
        _player.DamageChanged += DisplayPlayerDamage;
        _player.PotionCountChanged += DisplayPlayerPotionCount;
        _player.ExperienceChanged += DisplayPlayerExperience;
        _player.LevelChanged += DisplayPlayerLevel;
    }

    private void OnDisable()
    {
        _player.DamageChanged -= DisplayPlayerDamage;
        _player.PotionCountChanged -= DisplayPlayerPotionCount;
        _player.ExperienceChanged -= DisplayPlayerExperience;
        _player.LevelChanged -= DisplayPlayerLevel;
    }

    private void DisplayPlayerDamage(int damage)
    {
        _damageField.text = "x" + damage;
    }

    private void DisplayPlayerPotionCount(int potion)
    {
        _potionField.text = "x" + potion;
    }

    private void DisplayPlayerExperience(int currentExperience, int experienceToNextLevel)
    {
        _experienceField.text = "x" + currentExperience + "/" + experienceToNextLevel;
    }

    private void DisplayPlayerLevel(int level)
    {
        _levelField.text = "x" + level;
    }
}
