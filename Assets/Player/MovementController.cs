using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    //Class Declarations
    [SerializeField] private float speed;
    private PlayerInputActions _actions => InputRoot.Instance?.PlayerInput;
    private Rigidbody _rigidBody;
    private Vector3 _movementDirection;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _actions.BaseControls.Move.performed += OnMove;
        _actions.BaseControls.Move.canceled += OnMove;
    }

    private void FixedUpdate()
    {
        _rigidBody.linearVelocity = _movementDirection * speed;
    }


    //Helpers
    private void OnMove(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector3>();
    }
}
