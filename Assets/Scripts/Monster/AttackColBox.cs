using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColBox : MonoBehaviour
{
    public GameObject EnemyAttack = null; // 공격 col이 담긴 몬스터 자식 오브젝트
    public float EnemyATK = 0;    // 몬스터 공격력

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


