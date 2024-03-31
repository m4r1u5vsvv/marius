using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private PlayerInput inputActions;
    private CharacterController controller;
    private Animator animator;
    private Vector2 movementInput;
    private Vector3 currentMovement;
    private Quaternion rotateDir;
    private bool isRunning;
    private bool isWalking;

    private void OnMovementActions(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        currentMovement.x = movementInput.x;
        currentMovement.z = movementInput.y;
        isWalking = movementInput.x != 0 || movementInput.y != 0;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputActions = new PlayerInput();
        inputActions.CharacterControls.Movement.started += OnMovementActions;
        inputActions.CharacterControls.Movement.performed += OnMovementActions;
        inputActions.CharacterControls.Movement.canceled += OnMovementActions;
        inputActions.CharacterControls.Run.started += OnRun;
        inputActions.CharacterControls.Run.canceled += OnRun;
    }

    private void OnEnable()
    {
        inputActions.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        inputActions.CharacterControls.Disable();
    }

    private void PlayerRotate()
    {
        if (isWalking)
        {
            rotateDir = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(currentMovement), Time.deltaTime * rotateSpeed);
            transform.rotation = rotateDir;
        }
    }

    private void AnimateControl()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
    }

    private void Update()
    {
        AnimateControl();
        PlayerRotate();
    }

    private void FixedUpdate()
    {
        controller.Move(currentMovement * Time.fixedDeltaTime);
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        isRunning = context.ReadValueAsButton();
    }
    public void Respawn()
    {
        controller.enabled = false;
        transform.position = Vector3.up;
        controller.enabled = true;
    }
}
