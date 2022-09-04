using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] protected int MaxHealth;
    [SerializeField] private int _armor;
    [SerializeField] private int _baseDamage;
    [SerializeField] private UnityEvent _die;

    protected int CurrentHealth;

    public int BaseDamage => _baseDamage;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public bool Die()
    {
        bool die = CurrentHealth <= 0;
        return die;
    }

    public virtual void Dead()
    {
        Debug.Log("Кто-то умер.");
        _die?.Invoke();
    }

    public void ApplyDamage(int damage)
    {
        if (damage >= _armor)
        {
            damage -= _armor;
            _armor = 0;
            CurrentHealth -= damage;
        }
        else
        {
            _armor -= damage;
        }

        Info();
        if (Die()) Dead();
    }

    public void DealDamage(Fighter target)
    {
        int totalDamage = BaseDamage;
        target.ApplyDamage(totalDamage);
    }

    public void Info()
    {
        Debug.Log($"У игрока {_name} {CurrentHealth}/{MaxHealth} здоровья, {_armor} брони и урон в {_baseDamage} единиц!");
    }
}