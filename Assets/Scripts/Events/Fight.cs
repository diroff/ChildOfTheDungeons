using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fight : Event
{
    [SerializeField] private FreeItem _freeItemEvent;
    [SerializeField] private Continue _continue;

    [SerializeField] protected EventsController EventsController;
    [SerializeField] protected EnemyTemplates EnemyTemplates;

    [Header("Coroutine time")]
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

    [Header("Minigames")]
    [SerializeField] private AttackMinigame _attackMinigame;

    [Space]
    [SerializeField] private Animator _coinAnimator;

    private Enemy _enemy;
    private bool _enemyAssigned;

    private float _damage;

    private int _leaveChance;
    private int _coinWinChance;

    public override void StartEvent()
    {
        base.StartEvent();

        CreatingEnemy();
        StartCoinFlip();
        _attackMinigame.Ended.AddListener(PowerAttack);
    }

    public override void EndEvent()
    {
        if(_attackMinigame != null)
            _attackMinigame.Ended.RemoveListener(PowerAttack);

        _enemyAssigned = false;
        base.EndEvent();
        _enemyInfoButton.SetActive(false);
        _enemyInfoPanel.SetActive(false);

        if (_enemy != null)
            UnsubscribeEvents();
    }

    public void PlayerStep()
    {
        _damage = 0;
        _attackPanel.SetActive(true);
        _healButton.SetButtonState();
        CalculateLeaveChance();
        _leaveChangeText.text = _leaveChance + "%";
        _damageText.text = "x" + Player.CalculateTotalDamage(1);
    }

    public void EnemyStep()
    {
        StartCoroutine(AttackPlayer());
    }

    public void AttackEnemy()
    {
        StartCoroutine(AttackEnemyCoroutine(false));
    }

    public void StartAttackGame()
    {
        _attackPanel.SetActive(false);
        _attackMinigame.gameObject.SetActive(true);
    }

    public void PowerAttack()
    {
        _damage = Player.CalculateTotalDamage() * _attackMinigame.GetGameResult();
        _attackMinigame.gameObject.SetActive(false);
        StartCoroutine(AttackEnemyCoroutine(true));
    }

    public void Heal()
    {
        StartCoroutine(HealCoroutine());
    }

    public void Leave()
    {
        Player.TryToLeave(_leaveChance);
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
        _enemyAssigned = true;
    }


    private void SubscribeEvents()
    {
        _enemy.Died += EnemyDead;
        Player.Died += PlayerDead;
        Player.Leaved += PlayerLeaved;
        Player.NotLeaved += PlayerNotLeaved;
    }

    private void UnsubscribeEvents()
    {
        _enemy.Died -= EnemyDead;
        Player.Died -= PlayerDead;
        Player.Leaved -= PlayerLeaved;
        Player.NotLeaved -= PlayerNotLeaved;
    }

    protected void CreatingEnemy()
    {
        if (Spawner.GetEnemy() == null)
            SpawnEnemy();

        _enemy = Spawner.GetEnemy();
        SubscribeEvents();
    }

    private void SpawnEnemy()
    {
        if (_enemyAssigned)
        {
            Spawner.SpawnEnemy(_enemy);
            EnemyTemplates.SetLastEnemy(_enemy);
        }
        else
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
        _leaveChance = Player.Skills.Agility.CurrentLevel * 10;

        if (Player.CurrentFighterHealth <= Player.MaxFighterHealth / 4)
            _leaveChance += 30;

        if (_leaveChance > 100)
            _leaveChance = 100;
    }

    private void CalculateCoinWinChance()
    {
        _coinWinChance = Player.Skills.Luck.CurrentLevel * 10;

        if (Player.CurrentFighterHealth <= Player.MaxFighterHealth / 4)
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

    private IEnumerator AttackEnemyCoroutine(bool modificatedDamage)
    {
        if (!modificatedDamage)
            _damage = Player.CalculateTotalDamage();

        _attackPanel.SetActive(false);
        Player.Attack();

        _enemy.TakeDamage(_damage);
        yield return new WaitForSeconds(_timeAfterAttack);

        if (!_enemy.Die())
            EnemyStep();
    }

    private IEnumerator AttackPlayer()
    {
        _attackPanel.SetActive(false);
        _enemy.TryAttack(Player);
        yield return new WaitForSeconds(_timeAfterAttack);

        if (!Player.Die())
            PlayerStep();
    }

    private IEnumerator EnemyDeadCoroutine()
    {
        Player.AddExperience(_enemy.CalculateExperienceCost());
        SetPanelState(false);
        Spawner.PullOutGrave();

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
        Player.Heal();
        yield return new WaitForSeconds(_healingTime);
        _healButton.SetButtonState();
        EnemyStep();
    }

    private IEnumerator PlayerNotLeavedCoroutine()
    {
        _attackPanel.SetActive(false);
        _startPanel.SetActive(false);
        SetPanelState(false);
        yield return new WaitForSeconds(_timeBeforeLeave);
        SetPanelState(true);
        EnemyStep();
    }

    private IEnumerator FlipCoinCoroutine()
    {
        _startPanel.SetActive(false);
        _coinImage.SetActive(true);

        bool isWin = Player.HasAdditionalChance(_coinWinChance);

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
        EventsController.SetContinue(true);
        SetPanelState(false);
        EndEvent();
        _continue.ContinueWay();
    }

    private void PlayerNotLeaved()
    {
        StartCoroutine(PlayerNotLeavedCoroutine());
    }
}