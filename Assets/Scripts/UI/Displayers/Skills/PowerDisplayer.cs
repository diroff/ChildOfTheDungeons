public class PowerDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.Power.ValueChanged.AddListener(DisplayParameter);
    }
}