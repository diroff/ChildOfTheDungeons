using UnityEngine;

public class Player : Fighter
{
    public override void Dead()
    {
        Debug.Log("����� ����. ���� ��������!");
    }
}
