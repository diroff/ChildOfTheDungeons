using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyTemplates = new List<Enemy>();
    [SerializeField] private List<Item> _itemTemplates = new List<Item>();
    [SerializeField] private List<Chest> _chestTemplates = new List<Chest>();

    [SerializeField] private ProgressionController _progression;

    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _itemPlace;
    [SerializeField] private GameObject _enemyPlace;

    private int NumberOfRandomEnemy()
    {
        return Random.Range(0, _enemyTemplates.Count);
    }

    public void SpawnEnemy()
    {
        Enemy enemy = _enemyTemplates[NumberOfRandomEnemy()];
        enemy.SetLevel(_progression.SetLevel());
        Instantiate(enemy, _enemyPlace.transform);
    }

    public void SpawnItem(Item item = null)
    {
        if(item == null)
            item = _itemTemplates[NumberOfRandomItem()];

        item.SetLevel(_progression.SetLevel());
        Instantiate(item, _itemPlace.transform);
    }

    public void SpawnChest()
    {
        Instantiate (_chestTemplates[NumberOfRandomChest()], _enemyPlace.transform);
    }

    private int NumberOfRandomItem()
    {
        return Random.Range(0, _itemTemplates.Count);
    }

    public int NumberOfRandomChest()
    {
        return Random.Range(0, _chestTemplates.Count);
    }

    public Item GetItem()
    {
        return _itemPlace.GetComponentInChildren<Item>();
    }

    public Enemy GetEnemy()
    {
        return _enemyPlace.GetComponentInChildren<Enemy>();
    }

    public Chest GetChest()
    {
        return _enemyPlace.GetComponentInChildren<Chest>();
    }
}