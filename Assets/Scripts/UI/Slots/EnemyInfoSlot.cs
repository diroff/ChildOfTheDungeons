public class EnemyInfoSlot : Slot
{
    private void OnEnable()
    {
        IsFilled = true;
    }

    public override void ShowDescription()
    {
        base.ShowDescription();
        gameObject.SetActive(false);
    }
}