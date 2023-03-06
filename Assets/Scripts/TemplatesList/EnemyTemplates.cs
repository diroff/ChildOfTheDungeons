using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplates : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyTemplates;
    [SerializeField] private ProgressionController _progression;

    private Enemy _lastEnemy;
    private Enemy _lastBoss;
    private List<Enemy> _availableEnemies = new List<Enemy>();
    private List<Enemy> _availableBosses = new List<Enemy>();

    public Enemy LastEnemy => _lastEnemy;
    public Enemy LastBoss => _lastBoss;
    public List<Enemy> AvailableEnemies => _availableEnemies;
    public List<Enemy> AvailableBosses => _availableBosses;

    private void Awake()
    {
        UpdateEnemyList(0);
    }

    private void OnEnable()
    {
        _progression.Player.LevelChanged.AddListener(UpdateEnemyList);
    }

    public void UpdateEnemyList(int level)
    {
        _availableEnemies.Clear();

        foreach (Enemy enemy in _enemyTemplates)
        {
            if (_progression.Player.GetLevel() == enemy.MinimalLevel)
            {
                if (enemy.IsBoss)
                    _availableBosses.Add(enemy);
                else
                    _availableEnemies.Add(enemy);
            }
        }
    }

    public Enemy TakeEnemy(bool isBoss)
    {
        Enemy enemy;

        if (isBoss)
        {
            enemy = _availableBosses[GetEnemyNumber(true)];

            while (enemy == _lastEnemy)
            {
                if (_availableBosses.Count == 1)
                    break;

                enemy = _availableBosses[GetEnemyNumber(true)];
            }

            _lastBoss = enemy;
            return enemy;
        }
        else
        {
            enemy = _availableEnemies[GetEnemyNumber(false)];

            while (enemy == _lastEnemy)
            {
                if (_availableEnemies.Count == 1)
                    break;
                enemy = _availableEnemies[GetEnemyNumber(false)];
            }

            _lastEnemy = enemy;
            return enemy;
        }
    }

    private int GetEnemyNumber(bool isBoss)
    {
        int enemyNumber;

        if (isBoss)
            enemyNumber = Random.Range(0, _availableBosses.Count);
        else
            enemyNumber = Random.Range(0, _availableEnemies.Count);

        return enemyNumber;
    }
}