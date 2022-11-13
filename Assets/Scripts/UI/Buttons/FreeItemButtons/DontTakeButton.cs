using UnityEngine;

public class DontTakeButton : ButtonAction
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;

    public override void OnClickAction()
    {
        Item item = _spawner.GetItem();
        item.TakeItem();

        Destroy(item.gameObject);
    }
}