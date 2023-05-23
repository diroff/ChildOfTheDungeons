public class LuckDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.Luck.ValueChanged.AddListener(DisplayParameter);
    }
}