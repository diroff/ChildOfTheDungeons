using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthChanger : MonoBehaviour
{
    [SerializeField] private Fighter _startTarget;
    [SerializeField] private UnityAction _heal;
    [SerializeField] private SceneStates _sceneStates;

    private Fighter _endTarget;

    private void Start()
    {
        _endTarget = _sceneStates.TakeEnemy();
    }

    public void Attack()
    {
        _endTarget = _sceneStates.TakeEnemy();
        _startTarget.DealDamage(_endTarget);
    }

    public void Heal()
    {
        _heal?.Invoke();
    }
}
