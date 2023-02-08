using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _doorIcon;

    public void SetIcon(Sprite sprite)
    {
        _doorIcon.sprite = sprite;
    }
}
