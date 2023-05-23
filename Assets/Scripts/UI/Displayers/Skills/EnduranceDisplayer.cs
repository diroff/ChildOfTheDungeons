public class EnduranceDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.Endurance.ValueChanged.AddListener(DisplayParameter);
    }
}