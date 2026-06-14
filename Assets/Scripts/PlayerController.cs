using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform fpsCamera;
    
    [Header("VALUES")]
    [SerializeField] private float playerMovementSpeed = 2f;

    [Header("FIELDS")]
    private Vector3 forward;
    private Vector3 right;
    private Vector3 playerMovementInput;
    private Vector3 playerMovementDirection;
    private CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        forward = fpsCamera.transform.forward;
        right = fpsCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        playerMovementInput = gameInput.GetPlayerMovementInput();

        playerMovementDirection = (forward * playerMovementInput.y + right * playerMovementInput.x).normalized;

        if(playerMovementInput.magnitude > 0.001f)
        {
            characterController.Move(playerMovementDirection * Time.deltaTime * playerMovementSpeed);
        }

    }



}
