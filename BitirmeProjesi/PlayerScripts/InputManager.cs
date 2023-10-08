using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput PlayerInput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }
    public bool Crouch { get; private set; }
    public bool Reload { get; private set; }
    public bool GunPlay { get; private set; }
    public bool Jump { get; private set; }
    public bool Aim { get; private set; }

    private InputActionMap _currentMap;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _runAction;
    private InputAction _crouchAction;
    private InputAction _reload;
    private InputAction _gunPlay;
    private InputAction _jump;
    private InputAction _aim;

    private void Awake()
    {
        HideCursor();
        _currentMap = PlayerInput.currentActionMap;
        _moveAction = _currentMap.FindAction("Move");
        _lookAction = _currentMap.FindAction("Look");
        _runAction = _currentMap.FindAction("Run");
        _crouchAction = _currentMap.FindAction("Crouch");
        _reload = _currentMap.FindAction("Reload");
        _gunPlay = _currentMap.FindAction("GunPlay");
        _jump = _currentMap.FindAction("Jump");
        _aim = _currentMap.FindAction("Aim");

        _moveAction.performed += onMove;
        _lookAction.performed += onLook;
        _runAction.performed += onRun;
        _crouchAction.started += onCrouch;
        _reload.started += onReload;
        _gunPlay.started += onGunPlay;
        _jump.started += onJump;
        _aim.started += onAim;

        _moveAction.canceled += onMove;
        _lookAction.canceled += onLook;
        _runAction.canceled += onRun;
        _crouchAction.canceled += onCrouch;
        _reload.canceled += onReload;
        _gunPlay.canceled += onGunPlay;
        _jump.canceled += onJump;
        _aim.canceled += onAim;
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void onMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void onLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    private void onRun(InputAction.CallbackContext context)
    {
        Run = context.ReadValueAsButton();
    }

    private void onCrouch(InputAction.CallbackContext context)
    {
        Crouch = context.ReadValueAsButton();
    }

    private void onReload(InputAction.CallbackContext context)
    {
        Reload = context.ReadValueAsButton();
    }

    private void onGunPlay(InputAction.CallbackContext context)
    {
        GunPlay = context.ReadValueAsButton();
    }

    private void onJump(InputAction.CallbackContext context)
    {
        Jump = context.ReadValueAsButton();
    }

    private void onAim(InputAction.CallbackContext context)
    {
        Aim = context.ReadValueAsButton();
    }

    private void OnEnable()
    {
        _currentMap.Enable();
    }

    private void OnDisable()
    {
        _currentMap.Disable();
    }
}
