using UnityEngine;
using UnityEngine.Events;

public abstract class Fighter : MonoBehaviour
{    
    [SerializeField] protected string Name;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected int Armor;
    [SerializeField] protected int BaseDamage;
    [SerializeField] protected int Level;
    [SerializeField] protected Sprite SpriteImage;

    protected int CurrentHealth;

    public int baseDamage => BaseDamage;
    public event UnityAction<bool> Died;

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
        Info();
    }

    public bool Die()
    {
        bool die = CurrentHealth <= 0;
        return die;
    }

    public virtual void Dead()
    {
        Died?.Invoke(true);
    }

    public void TakeDamage(int damage)
    {
        if (damage >= Armor)
        {
            damage -= Armor;
            CurrentHealth -= damage;
        }

        Info();
        if (Die()) Dead();
    }

    public void Info()
    {
        Debug.Log($"” игрока {Name} {CurrentHealth}/{MaxHealth} здоровь€, {Armor} брони и урон в {BaseDamage} единиц!");
    }
}