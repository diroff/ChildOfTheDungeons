using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplayer : Displayer
{
    [SerializeField] private TextMeshProUGUI _nextLevelText;
    [SerializeField] private TextMeshProUGUI _currentExperience;
    [SerializeField] private Slider _experienceSlider;

    private void OnEnable()
    {
        Player.LevelChanged += DisplayParameter;
        Player.ExperienceChanged += DisplayExperience;
    }

    private void OnDisable()
    {
        Player.LevelChanged -= DisplayParameter;
        Player.ExperienceChanged -= DisplayExperience;
    }

    protected override void DisplayParameter(int value)
    {
        TextField.text = $"{value}";
        _nextLevelText.text = $"{value + 1}";
    }

    private void DisplayExperience(int currentExperience, int needExperience)
    {
        _currentExperience.text = $"{currentExperience}/{needExperience}";
        _experienceSlider.value = (float)currentExperience / needExperience;
        Debug.Log(_experienceSlider.value);
    }
}
