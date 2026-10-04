using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;

    private InputSystem_Actions inputActions;
    private Camera interactionCamera;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        interactionCamera = GetComponent<Camera>();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        // Eキーを押した場合
        if (inputActions.Player.Interact.WasPressedThisFrame())
        {
            TryInteract();
        }
    }

    // 正面の操作対象を検出して処理を実行する
    private void TryInteract()
    {
        Ray ray = interactionCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
