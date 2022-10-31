using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyTemplates = new List<Enemy>();
    [SerializeField] private List<Item> _itemTemplates = new List<Item>();

    [SerializeField] private GameObject _spawnPoint;

    public int NumberOfRandomEnemy()
    {
        return Random.Range(0, _enemyTemplates.Count);
    }

    public Enemy GetEnemy()
    {
        return GetComponentInChildren<Enemy>();
    }

    [ContextMenu("Spawn Enemy")]
    public void SpawnEnemy()
    {
        Enemy enemy = _enemyTemplates[NumberOfRandomEnemy()];
        Instantiate(enemy, _spawnPoint.transform);
    }
}