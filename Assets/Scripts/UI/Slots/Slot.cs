using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] protected Image SlotSprite;
    [SerializeField] protected InfoPanel InfoPanel;

    protected bool IsFilled = false;

    public bool IsItemFilled => IsFilled;

    public void UpdateSprite(Sprite sprite)
    {
        if (SlotSprite != null)
            SlotSprite.sprite = sprite;
    }

    public virtual void ShowDescription()
    {
        if (!SlotIsFilled())
            return;

        if (InfoPanel.gameObject.activeSelf)
            InfoPanel.gameObject.SetActive(false);

        InfoPanel.ShowInfo(true);
    }

    protected bool SlotIsFilled()
    {
        if (!IsFilled)
            return false;

        return true;
    }
}