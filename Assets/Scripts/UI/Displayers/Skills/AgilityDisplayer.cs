public class AgilityDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.Agility.ValueChanged.AddListener(DisplayParameter);
    }
}