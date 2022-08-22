using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _health;
    [SerializeField] private int _armor;
    [SerializeField] private int _baseDamage;
    [SerializeField] private UnityEvent _die;

    private bool Die()
    {
        bool die = _health <= 0;
        return die;
    }

    public virtual void Dead()
    {
        Debug.Log("���-�� ����.");
        _die?.Invoke();
    }

    public void ApplyDamage(int damage)
    {
        if (damage >= _armor)
        {
            damage -= _armor;
            _armor = 0;
            _health -= damage;
        }
        else
        {
            _armor -= damage;
        }

        Info();
        if (Die()) Dead();
    }

    public void Info()
    {
        Debug.Log($"� ������ {_name} {_health} ��������, {_armor} ����� � ���� � {_baseDamage} ������!");
    }
}
