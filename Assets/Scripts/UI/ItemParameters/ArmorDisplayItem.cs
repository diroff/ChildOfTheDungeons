using TMPro;
using UnityEngine;

public class ArmorDisplayItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private TextMeshProUGUI _armorText;

    private void OnEnable()
    {
        DisplayArmor();
    }

    public void DisplayArmor()
    {
        _armorText.text = "" + _item.GetComponent<Armor>().CalculateProtection();
    }
}
