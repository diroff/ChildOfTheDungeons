using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : Event
{
    [SerializeField] private Player _player;

    private Enemy _enemy;

    public override void DoEventSteps()
    {
        base.DoEventSteps();
        Spawner.SpawnEnemy();
        _enemy = Spawner.GetEnemy();
        _enemy.Died += DestroyEnemy;
        _player.Died += PlayerDead;
    }

    public override void EndEvent()
    {
        base.EndEvent();
        _enemy.Died -= DestroyEnemy;
        _player.Died -= PlayerDead;
    }

    private void DestroyEnemy(bool isDie)
    {
        if (isDie)
        {
            Destroy(_enemy.gameObject);
            EndEvent();
        }
    }

    private void PlayerDead(bool isDie)
    {
        if (isDie)
        {
            Destroy(_player.gameObject);
            EndEvent();
        }
    }
}