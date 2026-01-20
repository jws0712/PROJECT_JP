using UnityEngine;

public class Player : MonoBehaviour
{
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
        
    }

    private void Move()
    {

    }

    private void Jump()
    {

    }
}
