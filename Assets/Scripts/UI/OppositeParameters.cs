using System.Collections.Generic;
using UnityEngine;

public class OppositeParameters : MonoBehaviour
{
    [SerializeField] private GameObject _parameters;

    public void DisplayParameters(bool isEnable)
    {
        _parameters.SetActive(isEnable);
    }
}
