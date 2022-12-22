using System.Collections;
using TMPro;
using UnityEngine;

public class Fight : Event
{
    [SerializeField] private Player _player;

    [SerializeField] private GameObject _healthPanel;
    [SerializeField] private TextMeshProUGUI _healthText;

    private Enemy _enemy;

    public override void StartEvent()
    {
        base.StartEvent();
        Spawner.SpawnEnemy();
        _enemy = Spawner.GetEnemy();
        SetEnemyLevel();
        _healthPanel.SetActive(true);
        SubscribeEvents();
        PlayerStep();
    }

    public override void EndEvent()
    {
        base.EndEvent();
        _healthPanel.SetActive(false);
        UnsubscribeEvents();
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
        yield return new WaitForSeconds(1f);
        _enemy.TakeDamage(_player.CalculateTotalDamage());
        yield return new WaitForSeconds(0.5f);
        EnemyStep();
    }

    private IEnumerator AttackPlayer()
    {
        SetPanelState(false);
        yield return new WaitForSeconds(1f);
        _enemy.TryAttack(_player);
        yield return new WaitForSeconds(0.5f);
        if (!_enemy.Die()) 
            PlayerStep();
    }

    private void SubscribeEvents() 
    {
        _enemy.HealthChanged += HealthChanged;
        _enemy.Died += EnemyDead;
        _player.Died += PlayerDead;
        _player.Leaved += PlayerLeaved;
    }

    private void UnsubscribeEvents()
    {
        _enemy.HealthChanged -= HealthChanged;
        _enemy.Died -= EnemyDead;
        _player.Died -= PlayerDead;
        _player.Leaved -= PlayerLeaved;
    }

    private void EnemyDead(bool isDie)
    {
        if (isDie)
        {
            _player.AddExperience(_enemy.CalculateExperienceCost());
            Destroy(_enemy.gameObject);
            EndEvent();
        }
    }

    private void PlayerDead(bool isDie)
    {
        if (isDie)
        {
            Destroy(_player.gameObject);
            EndEvent();
        }
    }

    private void PlayerLeaved()
    {
        Destroy(_enemy.gameObject);
        EndEvent();
    }

    private void HealthChanged(int health)
    {
        _healthText.text = "x" + health;
    }

    private void SetEnemyLevel()
    {
        _enemy.SetLevel(Random.Range(_player.GetLevel(), _player.GetLevel() + 2));
    }
}