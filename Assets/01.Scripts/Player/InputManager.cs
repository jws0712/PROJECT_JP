using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [field: SerializeField] public Vector2 MoveInputVec {  get; private set; }
    [field: SerializeField] public Vector2 LookInputVec {  get; private set; }
    [field: SerializeField] public bool isInputJump { get; private set; }
    [field: SerializeField] public bool isInputSprint { get; private set; }

    private void OnMove(InputValue value)
    {
        MoveInputVec = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        LookInputVec = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        SetJumpState(value.isPressed);
    }
    
    private void OnSprint(InputValue value)
    {
        isInputSprint = value.isPressed;
    }


    public void SetJumpState(bool state)
    {
        isInputJump = state;
    }
}
