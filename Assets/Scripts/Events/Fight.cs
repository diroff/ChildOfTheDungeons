using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fight : Event
{
    [SerializeField] private Player _player;
    [SerializeField] private FreeItem _freeItemEvent;
    
    [SerializeField] protected EventsController EventsController;
    [SerializeField] protected EnemyTemplates EnemyTemplates;

    [Header("Events time")]
    [SerializeField] private float _timeBeforeAttack = 0.5f;
    [SerializeField] private float _timeAfterAttack = 0.5f;
    [SerializeField] private float _timeFromDead = 1.5f;
    [SerializeField] private float _timeBeforeLeave = 1.0f;
    [SerializeField] private float _healingTime = 1.0f;
    [SerializeField] private float _coinFlipTime = 3.0f;

    [Header("Attack buttons")]
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _leaveButton;
    [SerializeField] private HealSlot _healButton;
    [SerializeField] private TextMeshProUGUI _leaveChangeText;
    [SerializeField] private TextMeshProUGUI _damageText;

    [Header("Start panel")]
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _coinImage;
    [SerializeField] private Button _coinFlipButton;
    [SerializeField] private TextMeshProUGUI _leaveChangeStartText;
    [SerializeField] private TextMeshProUGUI _coinWinChangeText;

    [Header("Panels")]
    [SerializeField] private GameObject _attackPanel;
    [SerializeField] private GameObject _enemyInfoButton;
    [SerializeField] private GameObject _enemyInfoPanel;

    [Space]
    [SerializeField] private Animator _coinAnimator;

    private Enemy _enemy;
    private int _leaveChance;
    private int _coinWinChance;

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

        if(_enemy != null)
            UnsubscribeEvents();
    }

    public void PlayerStep()
    {
        _attackPanel.SetActive(true);
        _healButton.SetButtonState();
        CalculateLeaveChance();
        _leaveChangeText.text = _leaveChance + "%";
        _damageText.text = "x" + _player.CalculateTotalDamage();
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

    public void Leave()
    {
        _player.TryToLeave(_leaveChance);
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

    protected void CreatingEnemy()
    {
        if(Spawner.GetEnemy() == null)
            SpawnEnemy();

        _enemy = Spawner.GetEnemy();
        SubscribeEvents();
    }

    private void SpawnEnemy()
    {
        Spawner.SpawnEnemy(EnemyTemplates.TakeEnemy());
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

    private void CalculateLeaveChance()
    {
        _leaveChance = _player.Skills.Agility.CurrentLevel * 10;

        if (_player.CurrentFighterHealth <= _player.MaxFighterHealth / 4)
            _leaveChance += 30;

        if (_leaveChance > 100)
            _leaveChance = 100;
    }

    private void CalculateCoinWinChance()
    {
        _coinWinChance = _player.Skills.Luck.CurrentLevel * 10;

        if (_player.CurrentFighterHealth <= _player.MaxFighterHealth / 4)
            _coinWinChance += 30;

        if (_coinWinChance > 100)
            _coinWinChance = 100;
    }

    public void CoinFlip()
    {
        StartCoroutine(FlipCoinCoroutine());
    }

    private void StartCoinFlip()
    {
        _attackPanel.SetActive(false);
        _coinImage.SetActive(false);
        _startPanel.SetActive(true);
        _enemyInfoButton.SetActive(false);
        _enemyInfoPanel.SetActive(true);

        CalculateLeaveChance();
        CalculateCoinWinChance();

        _leaveChangeStartText.text = _leaveChance + "%";
        _coinWinChangeText.text = _coinWinChance + "%";
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
        SetPanelState(false);

        yield return new WaitForSeconds(_timeFromDead);

        if (_enemy.IsLoot())
        {
            EventsController.SetContinue(false);
            EventsController.SetEvent(_freeItemEvent);
            _freeItemEvent.SpawnItem(_enemy.LootItem);

            EndEvent();
            EventsController.StartEvent();
        }
        else
        {
            EventsController.SetContinue(true);
            EndEvent();
        }
    }

    private IEnumerator PlayerDeadCoroutine()
    {
        SetPanelState(false);
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
        EventsController.SetContinue(true);
        SetPanelState(false);
        yield return new WaitForSeconds(_timeBeforeLeave);
        Destroy(_enemy.gameObject);
        EndEvent();
    }

    private IEnumerator PlayerNotLeavedCoroutine()
    {
        _attackPanel.SetActive(false);
        SetPanelState(false);
        yield return new WaitForSeconds(_timeBeforeLeave);
        SetPanelState(true);
        EnemyStep();
    }

    private IEnumerator FlipCoinCoroutine()
    {
        _startPanel.SetActive(false);
        _coinImage.SetActive(true);

        bool isWin = _player.HasAdditionalChance(_coinWinChance);

        if (isWin)
            _coinAnimator.SetTrigger("Win");
        else
            _coinAnimator.SetTrigger("Lose");

        yield return new WaitForSeconds(_coinFlipTime);
        _enemyInfoPanel.SetActive(true);
        _coinImage.SetActive(false);

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