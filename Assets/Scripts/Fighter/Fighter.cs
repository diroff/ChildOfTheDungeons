using UnityEngine;
using UnityEngine.Events;

public abstract class Fighter : MonoBehaviour
{    
    [SerializeField] protected string Name;
    [SerializeField] protected float BaseMaxHealth;
    [SerializeField] protected float Armor;
    [SerializeField] protected float BaseDamage;
    [SerializeField] protected int Level;
    [SerializeField] protected Sprite SpriteImage;

    [SerializeField] protected Animator FighterAnimator;

    protected float MaxHealth;
    protected float CurrentHealth;

    public float BaseFighterDamage => BaseDamage;
    
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
        CurrentHealth = 0;
        FighterAnimator.SetBool("Dead", true);
        Died?.Invoke(true);
    }

    public virtual void Attack()
    {
        FighterAnimator.SetTrigger("Attack");
    }

    public virtual void TakeDamage(float damage)
    {
        if (Armor >= 80)
            Armor = 80;

        float totalDamage = damage * (1 - Armor / 100);
        CurrentHealth -= totalDamage;

        FighterAnimator.SetTrigger("Hit");

        if (Die()) 
            Dead();
    }

    public virtual void CalculateMaxHealth()
    {
        MaxHealth = BaseMaxHealth;
    }

    public int GetLevel()
    {
        return Level;
    }

    public virtual void SetLevel(int currentLevel)
    {
        Level = currentLevel;
    }
}