using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer ItemSprite;
    [SerializeField] protected InfoPanel InfoPanel;

    protected Item Item;
    protected bool IsFilled = false;

    [ContextMenu("Update sprite")]
    public void UpdateSprite()
    {
        ItemSprite.sprite = Item.ItemSprite;
    }

    public virtual void AddItem(Item item)
    {
        Item = item;
        IsFilled = true;
        InfoPanel.ShowInfo(false);
        UpdateSprite();
    }

    public virtual void ShowDescription()
    {
        if (!SlotIsFilled())
            return;

        if(InfoPanel.gameObject.activeSelf)
            InfoPanel.gameObject.SetActive(false);

        if (Item != null)
            InfoPanel.SetInfo(Item.ItemDescription);

        InfoPanel.ShowInfo(true);
    }

    protected bool SlotIsFilled()
    {
        if (!IsFilled)
            return false;

        return true;
    }
}