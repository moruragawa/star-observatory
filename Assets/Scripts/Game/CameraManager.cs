using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera fixedCamera;

    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Interactor playerInteractor;

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
        // 固定視点でEscキーを押した場合、通常視点に戻る
        if (fixedCamera.enabled &&
            inputActions.Player.Cancel.WasPressedThisFrame())
        {
            SwitchToPlayer();
        }
    }

    // 通常視点に切り替える
    public void SwitchToPlayer()
    {
        fixedCamera.enabled = false;
        playerCamera.enabled = true;

        playerController.enabled = true;
        playerInteractor.enabled = true;
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
}
