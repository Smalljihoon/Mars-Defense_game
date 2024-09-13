using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Migo : Monster
{
    public Animator Ground_Migo_anim = null;

    public override void Update()
    {
        base.Update();

        if (this.EnemyCurHp <= 0)
        {
            Ground_Migo_anim.SetTrigger("GroundDie");
            isMove = false;
            isDie = true;
        }
    }

    public override void Attack()
    {
        base.Attack();

        if(Ground_Migo_anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            Ground_Migo_anim.SetTrigger("GroundAttack");
        }
        else
        {
            Ground_Migo_anim.CrossFade("attack", 0);
        }
        //isMove= true;
    }

    public override void EnemyMove()
    {
        if (this.EnemyCurHp > 0)
        {
            Ground_Migo_anim.SetTrigger("GroundMove");
            base.EnemyMove();
        }
    }

    public override void EnemyDamage(int bulletATK)
    {
        if (this.EnemyCurHp > 0)
        {
            Ground_Migo_anim.SetTrigger("GroundDamaged");
            base.EnemyDamage(bulletATK);
        }
    }
}
