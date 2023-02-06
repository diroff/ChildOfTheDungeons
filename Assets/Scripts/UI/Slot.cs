using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer ItemSprite;
    [SerializeField] protected InfoPanel InfoPanel;

    protected Item Item;
    protected bool IsFilled = false;

    private void UpdateSprite(Sprite sprite)
    {
        ItemSprite.sprite = sprite;
    }

    public virtual void AddItem(Item item)
    {
        Item = item;
        IsFilled = true;
        InfoPanel.ShowInfo(false);
        UpdateSprite(Item.ItemSprite);
    }

    public virtual void ShowDescription()
    {
        if (!SlotIsFilled())
            return;

        if(InfoPanel.gameObject.activeSelf)
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