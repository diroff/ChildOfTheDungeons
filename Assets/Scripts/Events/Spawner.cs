using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyTemplates = new List<Enemy>();
    [SerializeField] private List<Item> _itemTemplates = new List<Item>();

    [SerializeField] private GameObject _spawnPoint;

    private int NumberOfRandomEnemy()
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

    private int NumberOfRandomItem()
    {
        return Random.Range(0, _itemTemplates.Count);
    }

    public Item GetItem()
    {
        return GetComponentInChildren<Item>();
    }

    [ContextMenu("Spawn Item")]
    public void SpawnItem()
    {
        Item item = _itemTemplates[NumberOfRandomItem()];
        Instantiate(item, _spawnPoint.transform);
    }
}