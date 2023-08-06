public class CoinsDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.CoinsCountChanged += DisplayParameter;
    }

    private void OnDisable()
    {
        Player.CoinsCountChanged -= DisplayParameter;
    }
}