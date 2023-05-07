using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerCharge))]
[RequireComponent (typeof(PlayerMovement))]
public class PlayerInputManager : NetworkBehaviour
{
    private PlayerCharge _playerCharge;
    private PlayerMovement _playerMovement;
    private Vector3 _movementInput = new Vector3();
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerCharge = GetComponent<PlayerCharge>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        if (isLocalPlayer == false)
            return;

        MoveInput();
    }

    private void Start()
    {
        if (isLocalPlayer == false)
            return;

        _playerInput.Player.Charge.performed += ctx => ChargeInput();
        _playerInput.Player.OpenCursor.performed += ctx => OpenCursor();
    }

    private void MoveInput()
    {
        _movementInput = _playerInput.Player.Move.ReadValue<Vector2>();
        _playerMovement.Move(_movementInput);
    }

    private void ChargeInput()
    {
        _playerCharge.Charge();
    }

    private void OpenCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
