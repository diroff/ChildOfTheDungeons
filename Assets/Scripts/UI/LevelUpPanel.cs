using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _skillPointCount;
    [SerializeField] private Button _upgradeButton;

    [Header("Dialogue")]
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private Tutorial _tutorial;

    private Skills _skills;
    private Skill _currentSkill;

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

    public void ChooseSkill(Skill skill)
    {
        _currentSkill = skill;
        _skills.SetActiveSkill(skill);
        CheckSkillAvailable();

        if (skill != null)
            _tutorialManager.AddMessages(_currentSkill.Tutorial);
    }

    public void UpgradeSkill()
    {
        _skills.UpgradeSkill();
        CheckSkillAvailable();
    }

    private void CheckSkillAvailable()
    {
        if (_currentSkill != null && _currentSkill.CanBeUpgraded())
            _upgradeButton.interactable = true;
        else
            _upgradeButton.interactable = false;
    }

    private void EnablePanel(int currentLevel)
    {
        _tutorialManager.AddMessages(_tutorial);
        _panel.SetActive(true);
        _levelText.text = $"Уровень {currentLevel}!";
        ShowSkillsCount(_player.Skills.SkillCountForLevel);
        _skills.SkillPointCountChanged.AddListener(ShowSkillsCount);
        ChooseSkill(null);
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