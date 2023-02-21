using TMPro;
using UnityEngine;

public class DamageDisplayItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private TextMeshProUGUI _damageText;

    private void OnEnable()
    {
        _item.GetComponent<Weapon>().DamageChanged += DisplayDamage;
    }

    private void OnDisable()
    {
        _item.GetComponent<Weapon>().DamageChanged -= DisplayDamage;
    }

    public void DisplayDamage(float damage)
    {
        _damageText.text = $"{damage}";
    }
}