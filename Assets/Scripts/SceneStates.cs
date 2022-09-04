using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStates : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

    private UIController _uiController;

    private void Start()
    {
        _uiController = GetComponent<UIController>();
        StartState();
    }

    public void StartState()
    {
        _uiController.StartStateButtons();
    }

    public void PlayerAttackState()
    {
        _uiController.PlayerAttackStateButtons();
    }

    public void EnemyAttackState()
    {
        if (_enemy.Die())
            EnemyDeadState();
        else
        {
            _uiController.EnemyAttackStateButtons();
            _player.ApplyDamage(_enemy.BaseDamage);

            if (!_player.Die())
                _uiController.PlayerAttackStateButtons();
            else
                PlayerDeadState();
        }
    }

    public void EnemyDeadState()
    {
        _uiController.EnemyDeadStateButtons();
    }

    public void PlayerDeadState()
    {
        _uiController.PlayerDeadStateButtons();
    }
}
