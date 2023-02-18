using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoSlot : Slot
{
    [SerializeField] private Spawner _spawnPoint;

    private Enemy _enemy;

    private void OnEnable()
    {
        _enemy = _spawnPoint.GetEnemy();
    }

    public override void ShowDescription()
    {
        base.ShowDescription();
        
    }
}
