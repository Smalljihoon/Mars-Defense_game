using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Crab : Monster
{
    public Animator Crab_anim;  // 크랩 애니메이터

    public override void Start()
    {
        Crab_anim = GetComponent<Animator>();
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (this.EnemyCurHp <= 0 && !isDie)
        {
            Crab_anim.SetTrigger("CrabDie");
            isDie = true;
            isMove = false;
        }
    }

    public override void EnemyDamage(int bulletATK)
    {
        if (this.EnemyCurHp > 0 && !isDie)
        {
            if (Crab_anim.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            {
                Crab_anim.SetTrigger("CrabDamage");
            }
            else
            {
                Crab_anim.CrossFade("Damage", 0);
            }

            base.EnemyDamage(bulletATK);
        }
    }

    public override void Attack()
    {
        base.Attack();

        Crab_anim.SetTrigger("CrabAttack");
        //if (Crab_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        //{
        //    Crab_anim.SetTrigger("CrabAttack");
        //}
        //else
        //{
        //    Crab_anim.CrossFade("Attack", 0);
        //}
        //isMove = true;
    }

    public override void EnemyMove()
    {
        if (this.EnemyCurHp > 0)
        {
            Crab_anim.SetTrigger("CrabWalk");
            base.EnemyMove();
        }
    }

}
