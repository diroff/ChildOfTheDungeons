public class BossEvent : Fight
{
    protected override void SpawnEnemy()
    {
        Spawner.SpawnEnemy(EnemyTemplates.TakeEnemy());
    }
}