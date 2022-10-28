using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : Event
{
    private Spawner _spawner;
    private Enemy _enemy;

    private void Awake()
    {
        _spawner = FindObjectOfType<Spawner>();
        Debug.Log(_spawner.name);
    }

    private void Start()
    {
        _enemy = _spawner.GetEnemy();
        //_enemy.Died += DestroyEnemy; //пр
    }

    public override void DoEventSteps()
    {
        SetPanel(true);
        _spawner.SpawnEnemy(); //пр
    }

    public override void EndEvent()
    {
        SetPanel(false);
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
