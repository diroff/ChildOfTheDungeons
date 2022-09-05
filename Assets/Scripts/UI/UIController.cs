using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _attack;
    [SerializeField] private Button _heal;
    [SerializeField] private Button _leave;
    [SerializeField] private Button _open;
    [SerializeField] private Button _continue;

    [SerializeField] private Player _player;

    public void StartStateButtons()
    {
        _attack.gameObject.SetActive(false);
        _heal.gameObject.SetActive(false);
        _leave.gameObject.SetActive(false);
        _continue.gameObject.SetActive(false);
        _open.gameObject.SetActive(true);
    }

    public void PlayerAttackStateButtons()
    {
        _attack.gameObject.SetActive(true);
        _leave.gameObject.SetActive(true);
        _open.gameObject.SetActive(false);
        _continue.gameObject.SetActive(false);

        if (_player.PotionChecker())
            _heal.gameObject.SetActive(true);
        else
            _heal.gameObject.SetActive(false);
    }

    public void EnemyAttackStateButtons()
    {
        _attack.gameObject.SetActive(false);
        _heal.gameObject.SetActive(false);
        _leave.gameObject.SetActive(false);
        _open.gameObject.SetActive(false);
        _continue.gameObject.SetActive(false);
    }

    public void PlayerDeadStateButtons()
    {
        _attack.gameObject.SetActive(false);
        _heal.gameObject.SetActive(false);
        _leave.gameObject.SetActive(false);
        _open.gameObject.SetActive(false);
        _continue.gameObject.SetActive(true);
    }

    public void EnemyDeadStateButtons()
    {
        _attack.gameObject.SetActive(false);
        _heal.gameObject.SetActive(false);
        _leave.gameObject.SetActive(false);
        _open.gameObject.SetActive(false);
        _continue.gameObject.SetActive(true);
    }
}