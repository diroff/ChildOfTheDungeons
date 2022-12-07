using UnityEngine;
using UnityEngine.Events;

public abstract class Fighter : MonoBehaviour
{    
    [SerializeField] protected string Name;
    [SerializeField] protected int BaseMaxHealth;
    [SerializeField] protected int Armor;
    [SerializeField] protected int BaseDamage;
    [SerializeField] protected int Level;
    [SerializeField] protected Sprite SpriteImage;

    protected int MaxHealth;
    protected int CurrentHealth;

    public int baseDamage => BaseDamage;
    
    public event UnityAction<bool> Died;

    protected virtual void Start()
    {
        CalculateMaxHealth();
        CurrentHealth = MaxHealth;
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

    public virtual void TakeDamage(int damage)
    {
        if (damage >= Armor)
        {
            damage -= Armor;
            CurrentHealth -= damage;
        }

        if (Die()) Dead();
    }

    public void CalculateMaxHealth()
    {
        MaxHealth = BaseMaxHealth * Level;
    }

    public int GetLevel()
    {
        return Level;
    }

    public void SetLevel(int currentLevel)
    {
        Level = currentLevel;
    }
}