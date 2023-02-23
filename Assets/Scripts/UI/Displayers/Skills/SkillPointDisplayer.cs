public class SkillPointDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.SkillPointCountChanged.AddListener(DisplayParameter);
    }
}