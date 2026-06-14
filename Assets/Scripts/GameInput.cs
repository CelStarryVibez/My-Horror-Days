using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    [Header("Fields")]
    private PlayerInput playerInput;
    private const string MOVEMENT = "Movement";

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public Vector2 GetPlayerMovementInput()
    {
       return playerInput.actions.FindAction(MOVEMENT).ReadValue<Vector2>();

    }
}
