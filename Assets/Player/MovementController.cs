using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    //Class Declarations
    [SerializeField] private float speed;
    [SerializeField] private PlatformPositionProvider platformPositionProvider;
    [SerializeField] private PlayerCore player;
    private PlayerInputActions _actions => InputRoot.Instance?.PlayerInput;
    private Rigidbody _rigidBody;
    private bool _isMovementHeld;
    private Vector3? targetPosition;
    private float stopDistance = 0.1f;
    private Directions _lastDirectionSelected;

    private void Awake()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
    }

    private void OnEnable()
    {
        _actions.BaseControls.Movement.started += OnMove;
        _actions.BaseControls.Movement.canceled += OnMove;
    }

    private void OnDisable()
    {
        _actions.BaseControls.Movement.started -= OnMove;
        _actions.BaseControls.Movement.canceled -= OnMove;
    }

    private void Update()
    {
        if (_isMovementHeld && !player.IsMoving)
        {
            UpdateTarget();
        }
    }

    private void FixedUpdate()
    {

        if (!player.IsMoving)
            return;
        var target = targetPosition.Value;
        var position = _rigidBody.position;
        Vector3 toTarget = new Vector3(target.x - position.x, 0f, target.z - position.z);
        float distance = toTarget.magnitude;

        if (distance <= stopDistance)
        {
            player.Stop();
            _rigidBody.linearVelocity = Vector3.zero;
            _rigidBody.position = new Vector3(target.x, position.y, target.z);
            return;
        }

        Vector3 direction = toTarget.normalized;
        _rigidBody.linearVelocity = direction * speed;
    }

    //Helpers

    private void UpdateTarget()
    {
        if (!_isMovementHeld)
            return;
        Vector3? newPosition = platformPositionProvider.TryGetPossiblePosition(_lastDirectionSelected);
        if (newPosition == null)
        {
            Debug.Log("Didn't move");
            return;
        }
        Debug.Log("Should move to: " + newPosition);
        targetPosition = newPosition;
        player.StartMoving();
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("CANCELATION");
            _isMovementHeld = false;
            return;
        }
        if (player.IsMoving)
            return;
        if (context.started)
        {
            _isMovementHeld = true;

            Vector2 value = context.ReadValue<Vector2>();
            Directions direction = value switch
            {
                { x: 0, y: 1 } => Directions.Up,
                { x: 0, y: -1 } => Directions.Down,
                { x: 1, y: 0 } => Directions.Right,
                { x: -1, y: 0 } => Directions.Left,
                _ => Directions.None
            };
            _lastDirectionSelected = direction;
        }

    }
}
