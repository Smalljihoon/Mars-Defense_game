using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Migo : Monster
{
    public Animator Fly_Migo_anim = null;
    bool isFly = false;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        if(transform.position.y < 7)
        {
            isFly = true;
        }
        else if(transform.position.y > 10)
        {
            isFly = false;
        }
        
        if(isFly && !isDie)
        {
            UpFly();
        }
        
        if (this.EnemyCurHp <= 0 && !isDie)
        {
            Fly_Migo_anim.SetTrigger("FlyDie");
            isMove = false;
            isDie = true;
        }
    }

    public override void EnemyDamage(int bulletATK)
    {
        if (this.EnemyCurHp > 0 && !isDie)
        {
            Fly_Migo_anim.SetTrigger("FlyDamaged");
            base.EnemyDamage(bulletATK);
        }
    }

    public void UpFly()
    {
        Enemyrigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
    }

    public override void Attack()
    {
        base.Attack();
        if (Fly_Migo_anim.GetCurrentAnimatorStateInfo(0).IsName("fly_attack"))
        {
            Fly_Migo_anim.SetTrigger("FlyAttack");
        }
        else
        {
            Fly_Migo_anim.CrossFade("fly_attack", 0);
        }
        //isMove = true;
    }

    public override void EnemyMove()
    {
        if (this.EnemyCurHp > 0)
        {
            Fly_Migo_anim.SetTrigger("FlyMove");
            base.EnemyMove();
        }
    }
}
