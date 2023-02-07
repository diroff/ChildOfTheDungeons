using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fight : Event
{
    [SerializeField] private Player _player;

    [Header("Events time")]
    [SerializeField] private float _timeBeforeAttack = 0.5f;
    [SerializeField] private float _timeAfterAttack = 0.5f;
    [SerializeField] private float _timeFromDead = 1.5f;
    [SerializeField] private float _timeBeforeLeave = 1.0f;
    [SerializeField] private float _healingTime = 1.0f;
    [SerializeField] private float _coinFlipTime = 3.0f;

    [Header("Buttons")]
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _leaveButton;
    [SerializeField] private HealSlot _healButton;

    [Header("Panels")]
    [SerializeField] private GameObject _coinFlipPanel;
    [SerializeField] private GameObject _coinImage;
    [SerializeField] private Button _coinFlipButton;
    [SerializeField] private Animator _coinAnimator;
    [SerializeField] private GameObject _attackPanel;

    private Enemy _enemy;

    public override void StartEvent()
    {
        base.StartEvent();
        CreatingEnemy();
        StartCoinFlip();
    }

    public override void EndEvent()
    {
        base.EndEvent();
        UnsubscribeEvents();
    }

    public void PlayerStep()
    {
        _attackPanel.SetActive(true);
        _healButton.SetButtonState();
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

    public void CoinFlip()
    {
        StartCoroutine(FlipCoinCoroutine());
    }

    private void StartCoinFlip()
    {
        _coinFlipPanel.SetActive(true);
    }

    private IEnumerator AttackEnemyCoroutine()
    {
        _attackPanel.SetActive(false);
        _player.Attack();
        yield return new WaitForSeconds(_timeBeforeAttack);
        _enemy.TakeDamage(_player.CalculateTotalDamage());
        yield return new WaitForSeconds(_timeAfterAttack);

        if(!_enemy.Die())
            EnemyStep();
    }

    private IEnumerator AttackPlayer()
    {
        _attackPanel.SetActive(false);
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
        _attackPanel.SetActive(false);
        yield return new WaitForSeconds(_healingTime);
        _player.Heal();
        _healButton.SetButtonState();
        EnemyStep();
    }

    private IEnumerator PlayerLeavedCoroutine()
    {
        SetPanelState(false);
        yield return new WaitForSeconds(_timeBeforeLeave);
        Destroy(_enemy.gameObject);
        EndEvent();
    }

    private IEnumerator FlipCoinCoroutine()
    {
        _coinFlipButton.gameObject.SetActive(false);
        _coinImage.SetActive(true);

        int randomNumber = Random.Range(0, 100);

        bool isWin = randomNumber < 50;

        if(isWin)
            _coinAnimator.SetTrigger("Win");
        else
            _coinAnimator.SetTrigger("Lose");

        yield return new WaitForSeconds(_coinFlipTime);

        _coinFlipPanel.SetActive(false);

        if (isWin)
            PlayerStep();
        else
            EnemyStep();
    }

    private void PlayerLeaved()
    {
        StartCoroutine(PlayerLeavedCoroutine());
    }
}