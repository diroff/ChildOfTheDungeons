using UnityEngine;

public class Enemy : Fighter
{
    public override void Dead()
    {
        Debug.Log("���� ����! ������ ���� ������!");
    }
}
