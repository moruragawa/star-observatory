using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private TMP_Text actionText;

    private InputSystem_Actions inputActions;
    private Camera interactionCamera;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        interactionCamera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();

        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        Interactable interactable = GetTargetInteractable();
    
        UpdateInteractionPrompt(interactable);

        // Eキーを押した場合
        if (inputActions.Player.Interact.WasPressedThisFrame())
        {
            TryInteract(interactable);
        }
    }

    // 正面の調査対象を取得する
    private Interactable GetTargetInteractable()
    {
        Ray ray = interactionCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            
            if (interactable != null && interactable.isActiveAndEnabled)
            {
                return interactable;
            }
        }

        return null;
    }

    // 調査対象を見ている場合、プロンプトを表示する
    private void UpdateInteractionPrompt(Interactable interactable)
    {
        if (interactable != null
            && interactable.isActiveAndEnabled
            && interactable.CanInteract)
        {
            interactionPrompt.SetActive(true);
            actionText.text = interactable.GetInteractionText();
        }
        else
        {
            interactionPrompt.SetActive(false);
        }
    }

    // 正面の操作対象を検出して処理を実行する
    private void TryInteract(Interactable interactable)
    {
        if (interactable != null
            && interactable.isActiveAndEnabled
            && interactable.CanInteract)
        {
            interactable.Interact();
        }
    }
}
