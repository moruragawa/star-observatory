using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera fixedCamera;
    [SerializeField] private Camera telescopeCamera;

    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Interactor playerInteractor;

    [Header("Telescope")]
    [SerializeField] private TelescopeInteractable telescopeInteractable;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
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
        // 固定視点または望遠鏡視点でEscキーを押した場合、通常視点に戻る
        if ((fixedCamera.enabled || telescopeCamera.enabled) &&
            inputActions.Player.Cancel.WasPressedThisFrame())
        {
            SwitchToPlayer();
        }
    }

    // 通常視点に切り替える
    public void SwitchToPlayer()
    {
        fixedCamera.enabled = false;
        telescopeCamera.enabled = false;
        playerCamera.enabled = true;

        playerController.enabled = true;
        playerInteractor.enabled = true;

        telescopeInteractable.StopViewing();
    }

    // 固定視点に切り替える
    public void SwitchToFixed(Transform viewPoint)
    {
        playerCamera.enabled = false;

        fixedCamera.transform.SetPositionAndRotation(
            viewPoint.position,
            viewPoint.rotation);

        fixedCamera.enabled = true;

        playerController.enabled = false;
        playerInteractor.enabled = false;
    }

    // 望遠鏡視点に切り替える
    public void SwitchToTelescope()
    {
        playerCamera.enabled = false;
        fixedCamera.enabled = false;
        telescopeCamera.enabled = true;

        playerController.enabled = false;
        playerInteractor.enabled = false;
    }
}
