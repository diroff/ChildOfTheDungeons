using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CostDisplayEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TextMeshProUGUI _costText;

    private void OnEnable()
    {
        _enemy.CostChanged += OnCostChanged;
    }

    private void OnDisable()
    {
        _enemy.CostChanged -= OnCostChanged;
    }

    private void OnCostChanged(int cost)
    {
        _costText.text = "x" + cost;
    }
}
