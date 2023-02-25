using TMPro;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.LevelChanged.AddListener(EnablePanel);
    }

    private void EnablePanel(int currentLevel)
    {
        _panel.SetActive(true);
        _levelText.text = $"Уровень {currentLevel}!";
    }
}