using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyTemplates = new List<Enemy>();
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private SceneStates _sceneStates;

    private Enemy _enemy;

    private void Awake()
    {
        SpawnEnemy();
    }

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

    public void SpawnEnemy()
    {
        Enemy enemy = _enemyTemplates[RandomChooseEnemy() - 1];

        _enemy = Instantiate(enemy, _spawnPoint.transform);
    }

    public void DestroyEnemy()
    {
        Destroy(_sceneStates.TakeEnemy().gameObject);
    }
}