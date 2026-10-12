using UnityEngine;

public class TelescopeInteractable : Interactable
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Transform telescopeCamera;

    [Header("Telescope Controls")]
    [SerializeField] private float lookSensitivity = 0.5f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 30f;
    [SerializeField] private float minYaw = -90f;
    [SerializeField] private float maxYaw = 90f;

    private InputSystem_Actions inputActions;
    private float cameraPitch;
    private float cameraYaw;
    private float initialYaw;
    private bool isViewing;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        Vector3 angles = telescopeCamera.localEulerAngles;

        cameraPitch = angles.x > 180f
            ? angles.x - 360f
            : angles.x;
        
        cameraYaw = angles.y > 180f
            ? angles.y - 360f
            : angles.y;

        initialYaw = cameraYaw;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        if (!isViewing)
        {
            return;
        }

        Vector2 lookInput = inputActions.Player.Look.ReadValue<Vector2>();

        // 左右の視点操作
        cameraYaw += lookInput.x * lookSensitivity;
        cameraYaw = Mathf.Clamp(
            cameraYaw, initialYaw + minYaw, initialYaw + maxYaw);

        // 上下の視点操作
        cameraPitch -= lookInput.y * lookSensitivity;
        cameraPitch = Mathf.Clamp(
            cameraPitch, minPitch, maxPitch);

        telescopeCamera.localRotation =
            Quaternion.Euler(cameraPitch, cameraYaw, 0f);
    }

    public override void Interact()
    {
        if (!gameManager.IsTelescopeUsable)
        {
            Debug.Log("望遠鏡はまだ使用できません");
            return;
        }

        isViewing = true;

        // 望遠鏡視点に切り替える
        cameraManager.SwitchToTelescope();
    }

    public void StopViewing()
    {
        isViewing = false;
    }
}
