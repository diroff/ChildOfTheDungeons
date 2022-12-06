using TMPro;
using UnityEngine;

public class Fight : Event
{
    [SerializeField] private Player _player;

    [SerializeField] private GameObject _healthPanel;
    [SerializeField] private TextMeshProUGUI _healthText;

    private Enemy _enemy;

    public override void DoEventSteps()
    {
        base.DoEventSteps();
        Spawner.SpawnEnemy();
        _enemy = Spawner.GetEnemy();
        _healthPanel.SetActive(true);
        _enemy.HealthChanged += HealthChanged;
        _enemy.Died += DestroyEnemy;
        _player.Died += PlayerDead;
        _player.Leaved += PlayerLeaved;
    }

    public override void EndEvent()
    {
        base.EndEvent();
        _healthPanel.SetActive(false);
        _enemy.HealthChanged -= HealthChanged;
        _enemy.Died -= DestroyEnemy;
        _player.Died -= PlayerDead;
        _player.Leaved -= PlayerLeaved;
    }

    private void DestroyEnemy(bool isDie)
    {
        if (isDie)
        {
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
}