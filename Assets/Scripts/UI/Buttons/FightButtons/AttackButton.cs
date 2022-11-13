using UnityEngine;

public class AttackButton : ButtonAction
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;

    public override void OnClickAction()
    {
        var enemy = _spawner.GetEnemy();
        enemy.TakeDamage(_player.baseDamage);
        enemy.TryAttack(_player);
    }
}
