using TMPro;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _skillPointCount;
    [Header("Dialogue")]
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private Tutorial _tutorial;

    private Skills _skills;

    private void Awake()
    {
        _skills = _player.Skills;
    }

    private void OnEnable()
    {
        _player.LevelChanged.AddListener(EnablePanel);
        _skills.SkillPointOver.AddListener(DisablePanel);
    }

    private void OnDisable()
    {
        _player.LevelChanged.RemoveListener(EnablePanel);
        _skills.SkillPointOver.RemoveListener(DisablePanel);
    }

    private void EnablePanel(int currentLevel)
    {
        _tutorialManager.AddMessages(_tutorial);
        _panel.SetActive(true);
        _levelText.text = $"Уровень {currentLevel}!";
        ShowSkillsCount(_player.Skills.SkillCountForLevel);
        _skills.SkillPointCountChanged.AddListener(ShowSkillsCount);
    }

    private void DisablePanel(bool isEnough)
    {
        if (isEnough)
            return;

        _panel.SetActive(false);
        _skills.SkillPointCountChanged.RemoveListener(ShowSkillsCount);
    }

    private void ShowSkillsCount(int count)
    {
        _skillPointCount.text = "Очков:" + count;
    }
}