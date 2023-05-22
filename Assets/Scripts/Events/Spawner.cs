using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Templates")]
    [SerializeField] private ItemList _itemList;
    [SerializeField] private List<Chest> _chestTemplates;

    [Header("Places")]
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _itemPlace;
    [SerializeField] private GameObject _enemyPlace;

    [SerializeField] private Animator _graveAnimator;

    public void SpawnEnemy(Enemy enemy)
    {
        Instantiate(enemy, _enemyPlace.transform);
    }

    public void SpawnItem(Item item = null)
    {
        if (item == null)
            item = _itemList.TakeItem();

        Instantiate(item, _itemPlace.transform);
    }

    public void SpawnChest(Chest chest = null)
    {
        if (chest == null)
            chest = _chestTemplates[NumberOfRandomChest()];
            
        Instantiate(chest, _enemyPlace.transform);
    }

    public void SpawnSign(Sign sign)
    {
        Instantiate(sign, _enemyPlace.transform);
    }

    public void PullOutGrave()
    {
        _graveAnimator.SetTrigger("Pull");
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