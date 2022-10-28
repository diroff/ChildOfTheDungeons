using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    public void TryAttack(Player player)
    {
        if (!Die())
        {
            player.TakeDamage(baseDamage);
        }
    }
}