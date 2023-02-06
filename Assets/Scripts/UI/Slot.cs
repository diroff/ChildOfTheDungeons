using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _itemSprite;

    public void UpdateSprite(Sprite sprite)
    {
        _itemSprite.sprite = sprite;
    }
}
