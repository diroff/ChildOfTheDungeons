using TMPro;
using UnityEngine;

public class Displayer : MonoBehaviour
{
    [SerializeField] protected Player Player;
    [SerializeField] protected TextMeshProUGUI TextField;

    protected virtual void DisplayParameter(int value)
    {
        TextField.text = "x" + value;
    }
}