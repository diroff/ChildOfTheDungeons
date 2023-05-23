public class HealItem : Item
{
    protected override void OnEnable()
    {
        base.OnEnable();
        ItemType = TypeOfItems.heal;
    }
}