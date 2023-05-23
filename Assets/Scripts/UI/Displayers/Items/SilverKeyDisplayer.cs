public class SilverKeyDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Inventory.SilverKeyChanged += DisplayParameter;
    }

    private void OnDisable()
    {
        Player.Inventory.SilverKeyChanged -= DisplayParameter;
    }
}