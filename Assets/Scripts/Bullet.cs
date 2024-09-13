using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        // �Ѿ� ����
    //        Destroy(this.gameObject);

    //        // ������ ������
    //        Monster col = collision.gameObject.GetComponent<Monster>();
    //        col.EnemyDamage(Player.Instance.LauncherATK);
    //    }
    //    else if(collision.gameObject.tag != "Player")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            // �Ѿ� ����
            Destroy(Player.Instance.instantLauncher);

            Monster mon = other.gameObject.GetComponent<Monster>();
            mon.EnemyDamage(Player.Instance.LauncherATK);
        }
        else if (other.gameObject.tag != "Enemy")
        {
            Destroy(Player.Instance.instantLauncher);
        }
    }
}
