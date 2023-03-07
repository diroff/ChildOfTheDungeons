using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplates : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyTemplates;
    [SerializeField] private ProgressionController _progression;
    [SerializeField] private int _additionalLevel = 0;

    private Enemy _lastEnemy;
    private List<Enemy> _availableEnemies = new List<Enemy>();

    public Enemy LastEnemy => _lastEnemy;
    public List<Enemy> AvailableEnemies => _availableEnemies;

    private void OnEnable()
    {
        _progression.Player.LevelChanged.AddListener(UpdateEnemyList);
        UpdateEnemyList(_progression.Player.GetLevel());
    }

    public void UpdateEnemyList(int level)
    {
        _availableEnemies.Clear();

        foreach (Enemy enemy in _enemyTemplates)
        {
            if (_progression.Player.GetLevel() + _additionalLevel == enemy.MinimalLevel)
                _availableEnemies.Add(enemy);
        }
    }

    public Enemy TakeEnemy()
    {
        Enemy enemy;
        enemy = _availableEnemies[GetEnemyNumber()];

        while (enemy == _lastEnemy)
        {
            if (_availableEnemies.Count == 1)
                break;
            enemy = _availableEnemies[GetEnemyNumber()];
        }
        
        _lastEnemy = enemy;
        return enemy;
    }

    private int GetEnemyNumber()
    {
        int enemyNumber;
        enemyNumber = Random.Range(0, _availableEnemies.Count);
        return enemyNumber;
    }
}