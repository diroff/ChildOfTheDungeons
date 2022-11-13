using UnityEngine;

public class TakeButton : ButtonAction
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;

    public override void OnClickAction()
    {
        Item item = _spawner.GetItem();
        item.TakeItem();

        _player.AddItem(_spawner.GetItem());
        Destroy(item.gameObject);
    }
}