using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("���� ����")]
    public string Enemyname = string.Empty; // UI ü��ǥ�ö� ǥ���� �̸�
    public float EnemyCurHp = 0;     // ���� HP
    public float EnemyMaxHp = 0;
    public float EnemyMoveSpeed = 0;  // ���� �̵��ӵ�
    public float IncreaseHp = 2f;    // ���� ������ ü������ ����

    [Header("���� ����")]
    [SerializeField] private GameObject AttackBox = null;       //  ���ݽ� ���� colider�� �������ִ�  ���� �� �ڽĿ�����Ʈ
    public GameObject PlayerBase = null; // ��ǥ ���� (��ǥ��1)
    public GameObject Character = null;    // �÷��̾� (��ǥ��2)
    public float Moveradius = 10;    // Ž�� �ݰ�
    public float Attackradius = 5; // ���� �ݰ� 
    public LayerMask Movelayer;     // ���� ������ �� ������ ���̾� = �÷��̾�
    public LayerMask Attacklayer;   // ���� ���ݽ� ���� ���̾�   = �÷��̾� , ����

    [Header("�����")]
    public AudioSource E_source = null;
    public AudioClip E_walkSound = null;
    public AudioClip E_attackSound = null;
    public AudioClip E_dieSound = null;

    protected bool isSensor = false;    // ���� ���� bool����
    protected Vector3 Movedir = Vector3.zero;   // �̵� ���� ���� ����
    protected Vector3 MovePos = Vector3.zero;      // �̵��� ���� ���� ����
    protected Vector3 TargetPoint = Vector3.zero;   // Ÿ������ ���� ����
    protected Rigidbody Enemyrigid; // ���� ������ٵ�
    protected float Diedelay = 0f;      // ���Ͱ� ������ �ı��Ǳ���� ������ �ð�
    protected bool isDie = false;       // ���� ���� bool
    protected bool isMove = true;   //  �̵� ���� bool
    protected bool isStun = false;
    protected bool is_attack_player = false;    // �÷��̾ �����ϴ��� bool
    public bool isAttack = false;   // ���ݿ��� bool
    protected float OriginSpeed;    // �̵� ���ǵ� ����
    protected float StunTime = 0f;  // ���� �ǰݽ� ���Ͻð�

    public virtual void Start()
    {
        EnemyRoundHp(); // ���� ������ ���� �� ü�� ����
        EnemyCurHp = EnemyMaxHp;
        OriginSpeed = EnemyMoveSpeed;
        Enemyrigid = GetComponent<Rigidbody>();
    }

    public virtual void Update()
    {
        is_attack_player = false;
        if (isStun)
        {
            StunTime += Time.deltaTime;
        }

        Collider[] movequest = Physics.OverlapSphere(transform.position, Moveradius, Movelayer);    // layer = player
        Collider[] attackquest = Physics.OverlapSphere(transform.position, Attackradius, Attacklayer);  // layer = player, base
        isSensor = movequest.Length > 0;
        isAttack = attackquest.Length > 0;
        //���̾� ��ȣ�� ����
        int playerLayer = LayerMask.NameToLayer("Player");

        // attackquest �迭�� ��ȸ�Ͽ� ���̾� Ȯ��
        foreach (var collider in attackquest)
        {
            if (collider.gameObject.layer == playerLayer)
            {
                is_attack_player = true;
                break;  // �÷��̾ ã���� ���� ����
            }
        }

        // ������ ���� ���ǹ� (�̵������ݰ� > ���ݰ����ݰ�) 
        if (isAttack) // ���� ���� �� ���� ok
        {
            if (isSensor)   // �̵������� �÷��̾� ������ �Ǹ�
            {
                if (is_attack_player)   // ���ݴ���� �÷��̾���
                {
                    Attack();               
                    isMove = false;
                }
                else // �÷��̾����� �̵�
                {
                    isMove = true; // �̵� ok
                }
            }
            else // �÷��̾� ������ �ƴ� �������
            {
                Attack();
                isMove = false;
            }
        }
        else
        {
            isMove = true;
        }

        if (isMove) // �̵�
        {
            EnemyMove();
        }

        // ���� ����
        if (StunTime > 2.0f)
        {
            isMove = true;
            EnemyMoveSpeed = OriginSpeed;
            StunTime = 0f;
            isStun = false;
        }
    }

    //���� ����
    public void Die()       // �ִϸ��̼� Ŭ���� �Լ�
    {
        //Enemyrigid.constraints &= ~RigidbodyConstraints.FreezePositionY;
        GameManager.Instance.GetMoney();
        GameManager.Instance.CountKill += 1;
        GameManager.Instance.Kill += 1;
        Destroy(this.gameObject);
    }

    // ���� ��½� ���� ü�� ����
    public virtual void EnemyRoundHp()
    {
        EnemyMaxHp = Mathf.RoundToInt(EnemyMaxHp * Mathf.Pow(IncreaseHp, GameManager.Instance.Round - 1));
    }

    //���� ����
    public virtual void Attack()
    {
        if (is_attack_player)   // ���ݴ�� �÷��̾�
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Player.Instance.transform.GetChild(0).position - transform.position), 0.5f);
        }
        else        // ���ݴ�� ����
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Base.Instance.transform.GetChild(0).position - transform.position), 0.5f);
        }
        E_source.clip = E_attackSound;
        E_source.Play();
    }

    // ���� �̵�
    public virtual void EnemyMove()
    {
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

    // ������ �ǰ�       (������(��Ʈ��ĵ))
    public virtual void EnemyDamage(int bulletATK)   //  �÷��̾�� ȣ��
    {
        if(EnemyCurHp> 0)
        {
            isMove = false; // �������� ����!
            isStun = true;
            EnemyMoveSpeed = 0; // ���� => �̼� = 0
            EnemyCurHp = EnemyCurHp - bulletATK;   // ȣ�� �� ������ ���� ���ݷ¸�ŭ hp���� ��´�
        }
    }

    // ����� Scene üũ��
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, Moveradius);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, Attackradius);
    //}
}
