using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fight : Event
{
    [SerializeField] private Player _player;
    [SerializeField] private EventsController _eventsController;
    [SerializeField] private FreeItem _freeItemEvent;

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
    [SerializeField] private GameObject _attackPanel;
    [SerializeField] private GameObject _enemyInfoButton;
    [SerializeField] private GameObject _enemyInfoPanel;
    [SerializeField] private Button _coinFlipButton;
    [SerializeField] private Animator _coinAnimator;

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
        _enemyInfoButton.SetActive(false);
        _enemyInfoPanel.SetActive(false);
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
        _player.NotLeaved += PlayerNotLeaved;
    }

    private void UnsubscribeEvents()
    {
        _enemy.Died -= EnemyDead;
        _player.Died -= PlayerDead;
        _player.Leaved -= PlayerLeaved;
        _player.NotLeaved -= PlayerNotLeaved;
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
        _enemyInfoButton.SetActive(true);
        _attackPanel.SetActive(false);
        _coinImage.SetActive(false);
        _coinFlipPanel.SetActive(true);
        _coinFlipButton.gameObject.SetActive(true);
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
        _enemyInfoButton.SetActive(false);
        _enemyInfoPanel.SetActive(false);
        yield return new WaitForSeconds(_timeFromDead);

        if (_enemy.IsLoot())
        {
            _eventsController.SetContinue(false);
            _eventsController.SetEvent(_freeItemEvent);
            _freeItemEvent.SpawnItem(_enemy.LootItem);

            EndEvent();
            _eventsController.StartEvent();
        }
        else
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
        _player.Heal();
        yield return new WaitForSeconds(_healingTime);
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

    private IEnumerator PlayerNotLeavedCoroutine()
    {
        _attackPanel.SetActive(false);
        yield return new WaitForSeconds(_timeBeforeLeave);
        EnemyStep();
    }

    private IEnumerator FlipCoinCoroutine()
    {
        _coinFlipButton.gameObject.SetActive(false);
        _coinImage.SetActive(true);

        int additionalChance = 0;

        if (_player.AdditionalChance())
            additionalChance = 25;

        int randomNumber = Random.Range(0, 125);

        bool isWin = randomNumber + additionalChance >= 75;

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

    private void PlayerNotLeaved()
    {
        StartCoroutine(PlayerNotLeavedCoroutine());
    }
}