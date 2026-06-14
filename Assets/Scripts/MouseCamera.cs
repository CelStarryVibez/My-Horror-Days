using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCamera : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private Transform playerTransformBody;

    [Header("VALUES")]
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float mouseClampY = 80f;

    [Header("FIELDS")]
    private Vector2 mouseRotation;
    private Vector2 lookInput;
    private float pitch;
    private float targetYaw;
    private Quaternion targetYawRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        SetMouseInput();
        RotateCamera();
        RotatePlayerBody();
    }

    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        lookInput = callbackContext.ReadValue<Vector2>();

    }


    private void SetMouseInput()
    {
        mouseRotation.x += lookInput.x * mouseSensitivity;
        mouseRotation.y -= lookInput.y * mouseSensitivity;

        mouseRotation.y = Mathf.Clamp(mouseRotation.y, -mouseClampY, mouseClampY);

    }

    private void RotateCamera()
    {
        pitch = mouseRotation.y;

        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void RotatePlayerBody()
    {
        targetYaw = mouseRotation.x;

        targetYaw = Mathf.DeltaAngle(0f, targetYaw);

        targetYawRotation = Quaternion.Euler(0f, targetYaw, 0f);

        playerTransformBody.transform.localRotation = targetYawRotation;



    }


}
