using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Templates")]
    [SerializeField] private List<Enemy> _enemyTemplates = new List<Enemy>();
    [SerializeField] private List<Item> _itemTemplates = new List<Item>();
    [SerializeField] private List<Chest> _chestTemplates = new List<Chest>();

    [Space]
    [SerializeField] private ProgressionController _progression;

    [Header("Places")]
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _itemPlace;
    [SerializeField] private GameObject _enemyPlace;

    private int _enemyNumber;

    public void SpawnEnemy()
    {
        SetNumberEnemy();

        Enemy enemy = _enemyTemplates[_enemyNumber];
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

    public void SpawnSign(Sign sign)
    {
        Instantiate(sign, _enemyPlace.transform);
    }

    private void SetNumberEnemy()
    {
        _enemyNumber = Random.Range(0, _enemyTemplates.Count);

        while (_enemyTemplates[_enemyNumber].MinimalLevel > _progression.Player.GetLevel())
        {
            SetNumberEnemy();
        }
    }

    private int NumberOfRandomItem()
    {
        return Random.Range(0, _itemTemplates.Count);
    }

    public int NumberOfRandomChest()
    {
        return Random.Range(0, _chestTemplates.Count);
    }

    public Enemy GetEnemy()
    {
        return _enemyPlace.GetComponentInChildren<Enemy>();
    }

    public Item GetItem()
    {
        return _itemPlace.GetComponentInChildren<Item>();
    }

    public Chest GetChest()
    {
        return _enemyPlace.GetComponentInChildren<Chest>();
    }

    public Sign GetSign()
    {
        return _enemyPlace.GetComponentInChildren<Sign>();
    }
}