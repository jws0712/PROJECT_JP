using System.Buffers;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private Transform orientation;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float speedChangeRate;

    [Header("Vertical Movement")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private Transform groundedCheckTr;
    [SerializeField] private float groundedCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 moveDir;

    [SerializeField] private float verticalVelocity;
    private float speed;
    private float animBlend;

    private bool isGrounded;

    private CharacterController controller;
    private CameraController cameraController;
    private InputManager input;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraController = GetComponent<CameraController>();
        input = GetComponent<InputManager>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        MakeGravity();

        CheckGround();
        Move();
        Jump();
    }

    private void Move()
    {
        float targetSpeed = input.isInputSprint ? sprintSpeed : moveSpeed;

        if (input.MoveInputVec == Vector2.zero) targetSpeed = 0f; //캐릭터 정지

        float currentSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude; //현재 캐릭터의 속력

        float speedOffset = 0.1f; //속도값 오차범위
        float inputMagnitude = 1f; //입력값 크기

        
        if(currentSpeed < targetSpeed - speedOffset || currentSpeed > targetSpeed + speedOffset)
        {
            speed = Mathf.Lerp(currentSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate); //현재 속도값에서 달성해야하는 속도값으로 보간
            //콘솔같은 경우 입력세기에 따라 속도 조절

            //소수점 3자리까지만 계산함
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed; //속도 보정
        }

        speed = moveSpeed;

        moveDir = orientation.forward * input.MoveInputVec.y + orientation.right * input.MoveInputVec.x;
        controller.Move(moveDir * (targetSpeed * Time.deltaTime));
    }

    private void Jump()
    {
        if(isGrounded)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = gravity;
            }

            if(input.isInputJump)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity); //점프
            }
        }
        else
        {
            input.SetJumpState(false);
        }
    }

    private void MakeGravity()
    {
        verticalVelocity += gravity * Time.deltaTime;
        controller.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundedCheckTr.position, groundedCheckRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;

        Gizmos.DrawSphere(groundedCheckTr.position, groundedCheckRadius);
    }
}
