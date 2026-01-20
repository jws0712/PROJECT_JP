using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private float sensitivity;
    [SerializeField] private float angleLimit;

    private InputManager input;

    private float yRot;
    private float xRot;

    [SerializeField] private Transform orientation;

    private void Awake()
    {
        input = GetComponent<InputManager>();
    }

    private void LateUpdate()
    {
        float mouseX = input.LookInputVec.x * Time.deltaTime * sensitivity;
        float mouseY = input.LookInputVec.y * Time.deltaTime * sensitivity;

        yRot += mouseX;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -angleLimit, angleLimit);

        cameraHolder.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
