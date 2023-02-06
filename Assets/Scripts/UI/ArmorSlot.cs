using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _itemSprite;

    private Armor _armor;

    public void UpdateSprite(Sprite sprite)
    {
        _itemSprite.sprite = sprite;
    }

    public void AddItem(Armor armor)
    {
        _armor = armor;
        UpdateSprite(armor.ItemSprite);
    }
}
