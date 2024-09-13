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
            Movedir = (TargetPoint - transform.position).normalized;  // �̵� ���� ��ֶ�����

            if (isSensor)
            {
                TargetPoint = Player.Instance.transform.GetChild(0).position;       // Ÿ�� ���� = player
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Movedir), 0.1f);   // �̵��������� ȸ��
            }
            else
            {
                TargetPoint = PlayerBase.transform.position;                    // Ÿ�� ���� = base
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(PlayerBase.transform.position - this.transform.position), 0.1f);  // �̵��������� ȸ��
            }

            MovePos = Enemyrigid.position + Movedir * EnemyMoveSpeed * Time.deltaTime;  // �̵�
            Enemyrigid.MovePosition(MovePos);   // Enemyrigid.MovePosition()�� ���� �̵�
            E_source.clip = E_walkSound;
            E_source.Play();
        }
    }
}
