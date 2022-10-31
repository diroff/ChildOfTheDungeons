using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : Event
{
    private Enemy _enemy;

    public override void DoEventSteps()
    {
        base.DoEventSteps();
        Spawner.SpawnEnemy();
        _enemy = Spawner.GetEnemy();
        _enemy.Died += DestroyEnemy;
    }

    public override void EndEvent()
    {
        base.EndEvent();
        _enemy.Died -= DestroyEnemy;
    }

    private void DestroyEnemy(bool isDie)
    {
        if (isDie)
        {
            Destroy(_enemy.gameObject);
            EndEvent();
        }
    }
}
