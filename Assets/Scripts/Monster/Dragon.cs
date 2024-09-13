using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    public Animator Dragonanim = null;
    public ParticleSystem Flames = null;
    private bool isFly = false;

    public override void Start()
    {
        Flames.Stop();
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (isAttack && !isDie)
        {
            Flames.Play();
        }
        else
        {
            Flames.Stop();
        }

        if (transform.position.y < 20)
        {
            isFly= true;
            
        }
        else if (transform.position.y > 20)
        {
            isFly= false;
        }

        if(isFly && !isDie)
        {
            UpFly();
        }

        if (EnemyCurHp <= 0 && !isDie)
        {
            isMove = false;
            //Enemyrigid.constraints &= ~RigidbodyConstraints.FreezePositionY;
            Dragonanim.SetBool("isDie", true);
            isDie = true;
        }
    }

    private void UpFly()
    {
        Enemyrigid.AddForce(Vector3.up * 3, ForceMode.Impulse);
    }

    public override void EnemyMove()
    {
        if (this.EnemyCurHp > 0)
        {
            Dragonanim.SetTrigger("DragonMove");
            Movedir = (TargetPoint - transform.position).normalized;  // 이동 방향 노멀라이즈

            if (isSensor)
            {
                TargetPoint = Player.Instance.transform.GetChild(0).position;       // 타겟 지점 = player
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Movedir), 0.1f);   // 이동방향으로 회전
            }
            else
            {
                TargetPoint = PlayerBase.transform.position;                    // 타겟 지점 = base
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(PlayerBase.transform.position - this.transform.position), 0.1f);  // 이동방향으로 회전
            }

            MovePos = Enemyrigid.position + Movedir * EnemyMoveSpeed * Time.deltaTime;  // 이동
            Enemyrigid.MovePosition(MovePos);   // Enemyrigid.MovePosition()을 통해 이동
            E_source.clip = E_walkSound;
            E_source.Play();
        }
    }
}
