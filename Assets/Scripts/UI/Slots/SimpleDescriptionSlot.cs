using UnityEngine;

public class SimpleDescriptionSlot : Slot
{
    [TextArea(1, 2)]
    [SerializeField] private string _information;


    private void OnEnable()
    {
        IsFilled = true;
    }

    public override void ShowDescription()
    {
        InfoPanel.SetInfo(_information);
        base.ShowDescription();
    }
}
