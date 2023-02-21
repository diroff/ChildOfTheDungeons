using TMPro;
using UnityEngine;

public class ArmorDisplayItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private TextMeshProUGUI _armorText;

    private void OnEnable()
    {
        _item.GetComponent<Armor>().ProtectionChanged += DisplayArmor;
    }

    private void OnDisable()
    {
        _item.GetComponent<Armor>().ProtectionChanged -= DisplayArmor;
    }

    public void DisplayArmor(float protection)
    {
        _armorText.text = $"{protection}";
    }
}