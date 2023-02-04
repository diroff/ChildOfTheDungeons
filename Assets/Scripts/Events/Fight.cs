using System.Collections;
using TMPro;
using UnityEngine;

public class Fight : Event
{
    [SerializeField] private Player _player;

    [SerializeField] private float _timeBeforeAttack = 0.5f;
    [SerializeField] private float _timeAfterAttack = 0.5f;
    [SerializeField] private float _timeFromDead = 1.5f;
    [SerializeField] private float _timeBeforeLeave = 1.0f;

    private Enemy _enemy;

    private bool _isPlayerStep = true;

    public override void StartEvent()
    {
        base.StartEvent();
        CreatingEnemy();
    }

    public override void EndEvent()
    {
        base.EndEvent();
        UnsubscribeEvents();
        _isPlayerStep = true;
    }

    public void PlayerStep()
    {
        SetPanelState(true);
    }

    public void EnemyStep()
    {
        StartCoroutine(AttackPlayer());
    }

    public void AttackEnemy()
    {
        StartCoroutine(AttackEnemyCoroutine());
    }

    private IEnumerator AttackEnemyCoroutine()
    {
        SetPanelState(false);
        _player.Attack();
        yield return new WaitForSeconds(_timeBeforeAttack);
        _enemy.TakeDamage(_player.CalculateTotalDamage());
        yield return new WaitForSeconds(_timeAfterAttack);

        _isPlayerStep = false;
        StepChecker();
    }

    private IEnumerator AttackPlayer()
    {
        SetPanelState(false);
        yield return new WaitForSeconds(_timeBeforeAttack);
        _enemy.TryAttack(_player);
        yield return new WaitForSeconds(_timeAfterAttack);

        _isPlayerStep = true;
        StepChecker();
    }

    private void StepChecker()
    {
        if (_isPlayerStep)
        {
            if (!_player.Die())
                PlayerStep();
        }
        else
        {
            if (!_enemy.Die())
                EnemyStep();
        }
    }

    private void SubscribeEvents() 
    {
        _enemy.Died += EnemyDead;
        _player.Died += PlayerDead;
        _player.Leaved += PlayerLeaved;
    }

    private void UnsubscribeEvents()
    {
        _enemy.Died -= EnemyDead;
        _player.Died -= PlayerDead;
        _player.Leaved -= PlayerLeaved;
    }

    private void CreatingEnemy()
    {
        Spawner.SpawnEnemy();
        _enemy = Spawner.GetEnemy();
        SubscribeEvents();
    }

    private void EnemyDead(bool isDie)
    {
        if (isDie)
            StartCoroutine(EnemyDeadCoroutine());
    }

    private void PlayerDead(bool isDie)
    {
        if (isDie)
            StartCoroutine(PlayerDeadCoroutine());
    }

    private IEnumerator EnemyDeadCoroutine()
    {
        _player.AddExperience(_enemy.CalculateExperienceCost());
        yield return new WaitForSeconds(_timeFromDead);
        EndEvent();
    }

    private IEnumerator PlayerDeadCoroutine()
    {
        yield return new WaitForSeconds(_timeFromDead);
        EndEvent();
    }

    private IEnumerator PlayerLeavedCoroutine()
    {
        SetPanelState(false);
        yield return new WaitForSeconds(_timeBeforeLeave);
        Destroy(_enemy.gameObject);
        EndEvent();
    }

    private void PlayerLeaved()
    {
        StartCoroutine(PlayerLeavedCoroutine());
    }
}