using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplayEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void OnEnable()
    {
        _enemy.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _enemy.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int level)
    {
        _levelText.text = "x" + level;
    }
}
