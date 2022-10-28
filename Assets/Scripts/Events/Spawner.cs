using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyTemplates = new List<Enemy>();
    [SerializeField] private List<Item> _itemTemplates = new List<Item>();

    [SerializeField] private GameObject _spawnPoint;

    public int RandomChooseEnemy()
    {
        int countOfEnemy = 0;
        int numberOfEnemy;

        for (int i = 0; i <= _enemyTemplates.Count; i++)
        {
            countOfEnemy = i;
        }

        numberOfEnemy = Random.Range(1, countOfEnemy + 1);

        return numberOfEnemy;
    }

    public Enemy GetEnemy()
    {
        return GetComponentInChildren<Enemy>();
    }

    [ContextMenu("Spawn Enemy")]
    public void SpawnEnemy()
    {
        Enemy enemy = _enemyTemplates[RandomChooseEnemy() - 1];

        Instantiate(enemy, _spawnPoint.transform);
    }
}