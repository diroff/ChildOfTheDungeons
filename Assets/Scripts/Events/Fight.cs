using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fight : Event
{
    [SerializeField] private Player _player;

    [SerializeField] private float _timeBeforeAttack = 0.5f;
    [SerializeField] private float _timeAfterAttack = 0.5f;
    [SerializeField] private float _timeFromDead = 1.5f;
    [SerializeField] private float _timeBeforeLeave = 1.0f;
    [SerializeField] private float _healingTime = 1.0f;

    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _healButton;
    [SerializeField] private Button _leaveButton;

    private Enemy _enemy;

    public override void StartEvent()
    {
        base.StartEvent();
        CreatingEnemy();
        PotionSlotChecker();
    }

    public override void EndEvent()
    {
        base.EndEvent();
        UnsubscribeEvents();
    }

    public void PlayerStep()
    {
        SetPanelState(true);
        PotionSlotChecker();
    }

    public void EnemyStep()
    {
        StartCoroutine(AttackPlayer());
    }

    public void AttackEnemy()
    {
        StartCoroutine(AttackEnemyCoroutine());
    }

    public void Heal()
    {
        StartCoroutine(HealCoroutine());
    }

    private void PotionSlotChecker()
    {
        if (_player.PotionChecker())
            _healButton.gameObject.SetActive(true);
        else
            _healButton.gameObject.SetActive(false);
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

    private IEnumerator AttackEnemyCoroutine()
    {
        SetPanelState(false);
        _player.Attack();
        yield return new WaitForSeconds(_timeBeforeAttack);
        _enemy.TakeDamage(_player.CalculateTotalDamage());
        yield return new WaitForSeconds(_timeAfterAttack);

        if(!_enemy.Die())
            EnemyStep();
    }

    private IEnumerator AttackPlayer()
    {
        SetPanelState(false);
        yield return new WaitForSeconds(_timeBeforeAttack);
        _enemy.TryAttack(_player);
        yield return new WaitForSeconds(_timeAfterAttack);

        if(!_player.Die())
            PlayerStep();
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

    private IEnumerator HealCoroutine()
    {
        SetPanelState(false);
        yield return new WaitForSeconds(_healingTime);
        _player.Heal();
        PotionSlotChecker();
        EnemyStep();
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