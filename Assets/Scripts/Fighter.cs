using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour
{
    [SerializeField] protected string Name;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected int Armor;
    [SerializeField] protected int BaseDamage;
    [SerializeField] protected Sprite SpriteImage;

    [SerializeField] private UnityEvent _die;

    protected int CurrentHealth;

    public int baseDamage => BaseDamage;

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
        if (damage >= Armor)
        {
            damage -= Armor;
            Armor = 0;
            CurrentHealth -= damage;
        }
        else
        {
            Armor -= damage;
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
        Debug.Log($"У игрока {Name} {CurrentHealth}/{MaxHealth} здоровья, {Armor} брони и урон в {BaseDamage} единиц!");
    }
}