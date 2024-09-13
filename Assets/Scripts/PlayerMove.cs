using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private CharacterController controller;    // 플레이어 컨트롤러
    [SerializeField] GameObject PlayerHead;
    [SerializeField] float MoveSpeed;       // 움직임 속도
    [SerializeField] GameObject Cam;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float YmouseSensitivity = 0f;     // 마우스 민감도
    [SerializeField] float XmouseSensitivity = 0f;     // 마우스 민감도

    Animator anim;

    bool isWalk = false;                       // 애니메이션을 위한 bool
    bool isRun = false;
    bool isGrounded = true;                 // 땅인지 아닌지 체크
    bool isRoll = false;
    private bool canPlayAnimation = true;

    private float MouseX = 0f;
    private float MouseY = 0f;
    float memorySpeed = 0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        memorySpeed = MoveSpeed;
    }

    void Update()
    {
        /*
        // RaycastHit hit;

        //// Raycast를 수행 (origin에서 direction 방향으로 maxDistance만큼 Ray를 쏘고 충돌 여부 검사)
        //if (Physics.Raycast(playerOriginPos, Raydirection, out hit, dir))
        //{
        //    // 충돌된 오브젝트가 있다면 해당 오브젝트의 정보를 출력
        //    Debug.Log("Hit: " + hit.collider.name);

        //    // 충돌 지점 표시 (디버그용)
        //    Debug.DrawLine(playerOriginPos, hit.point, Color.red);
        //}
        //else
        //{
        //    // 충돌된 오브젝트가 없을 때
        //    Debug.Log("No hit detected");

        //    // Ray를 표시 (디버그용)
        //    Debug.DrawLine(playerOriginPos, playerOriginPos + Raydirection * dir, Color.red);
        //}
        */
        /*
        isGrounded = controller.isGrounded;
        if (isGrounded && jumpVelocity.y < 0)
        {
            jumpVelocity.y = -2f;
            Debug.Log("Grounded");
        }
        jumpVelocity.y += gravity * Time.deltaTime;
         */
        Move();
        Run();
        BasicMouseRotation();
        Roll();
    }

    private void Move()
    {
        
        if (isGrounded)
        {
            isWalk = true;
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 move = right * moveHorizontal + forward * moveVertical;

            controller.Move(move * MoveSpeed * Time.deltaTime);
        }
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 move = right * moveHorizontal + forward * moveVertical;

            controller.Move(move * MoveSpeed * 1.5f * Time.deltaTime);
        }
    }

    void Roll()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= 1.0f && !anim.IsInTransition(0))
        {
            canPlayAnimation = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canPlayAnimation)
        {
            anim.SetTrigger("Roll");
            canPlayAnimation = false;
        }
    }
    

    //private void Jump() // 점프 함수
    //{
    //    if (Input.GetButtonDown("Jump") && isGrounded)
    //    {
    //        Debug.Log("점프");
    //        jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    //        //isGrounded = false;
    //    }
    //    //PlayerRigid.AddForce(Vector3.up * JumpPower, ForceMode.Force);
    //}

    private void BasicMouseRotation()   // 기본 카메라 회전 함수
    {
        MouseX += Input.GetAxis("Mouse X") * YmouseSensitivity * Time.deltaTime;
        MouseY -= Input.GetAxis("Mouse Y") * YmouseSensitivity * Time.deltaTime;
        MouseY = Mathf.Clamp(MouseY, -90f, 90f);

        //cameraTransform.transform.RotateAround(PlayerHead.transform.position,  Vector3.back, MouseY);

        transform.localRotation = Quaternion.Euler(MouseY, MouseX, 0);
    }

    //private IEnumerator Jump()
    //{
    //    Vector3 endPosition = transform.position + (Vector3.up * 10);
    //    while (transform.position.y < endPosition  .y)
    //    {
    //        transform.position += Vector3.up;
    //        yield return null;
    //    }
    //}

    private void OnCollisionStay(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Ground")
        {
            //isGrounded = true;
        }
    }
}
