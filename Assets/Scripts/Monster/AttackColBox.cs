using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColBox : MonoBehaviour
{
    public GameObject EnemyAttack = null; // ���� col�� ��� ���� �ڽ� ������Ʈ
    public float EnemyATK = 0;    // ���� ���ݷ�

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Player.Instance.TakeDamage(EnemyATK);
            }
            else if(other.gameObject.CompareTag("Base"))
            {
                Base.Instance.HitBase(EnemyATK);
            }
        }
        else
            return;
    }
}


