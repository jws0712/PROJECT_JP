using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [field: SerializeField] public Vector2 MoveInputVec {  get; private set; }
    [field: SerializeField] public Vector2 LookInputVec {  get; private set; }
    [field: SerializeField] public bool isPressJump { get; private set; }
    [field: SerializeField] public bool isPressAttack { get; private set; }
    [field: SerializeField] public bool isPressSprint { get; private set; }

    private void OnMove(InputValue value)
    {
        MoveInputVec = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        LookInputVec = value.Get<Vector2>();
    }

    private void OnSprint(InputValue value)
    {
        isPressSprint = value.isPressed;
    }

    private void OnJump(InputValue value)
    {
        SetJumpState(value.isPressed);
    }

    private void OnAttack(InputValue value)
    {
        SetAttackState(value.isPressed);
    }

    public void SetJumpState(bool state)
    {
        isPressJump = state;
    }
    public void SetAttackState(bool state)
    {
        isPressAttack = state;
    }
}
