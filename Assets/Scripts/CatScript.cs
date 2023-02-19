using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        _panel.SetActive(false);
    }
}
