using TMPro;
using UnityEngine;

public class DamageDisplayItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private TextMeshProUGUI _damageText;

    private void OnEnable()
    {
        DisplayDamage();
    }

    public void DisplayDamage()
    {
        _damageText.text = "" + _item.GetComponent<Weapon>().CalculateDamage();
    }
}
