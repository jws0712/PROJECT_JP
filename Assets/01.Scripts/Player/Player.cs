using System.Buffers;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;

    [Header("Vertical Movement")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpCoolTime;
    [SerializeField] private Transform groundedCheckTr;
    [SerializeField] private float groundedCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;

    private CharacterController controller;
    private InputManager input;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<InputManager>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        CheckGround();
    }

    private void Move()
    {

    }

    private void Jump()
    {
        
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
