using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _info;

    public void ShowInfo(bool enabled)
    {
        _panel.SetActive(enabled);
    }

    public void SetInfo(string info, int value, int level)
    {
        _info.text = $"{info} \nDmg:{value}\nLvl:{level}";
    }

    public void SetInfo(string info)
    {
        _info.text = info;
    }
}
