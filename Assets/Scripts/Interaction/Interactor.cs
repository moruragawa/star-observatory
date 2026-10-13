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

        bool canInteract = interactable != null
            && interactable.isActiveAndEnabled
            && interactable.CanInteract;
    
        UpdateInteractionPrompt(interactable, canInteract);

        // Eキーを押した場合、インタラクション処理を実行する
        if (inputActions.Player.Interact.WasPressedThisFrame() && canInteract)
        {
            interactable.Interact();
        }
    }

    // 正面の調査対象を取得する
    private Interactable GetTargetInteractable()
    {
        Ray ray = interactionCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            return hit.collider.GetComponent<Interactable>();
        }

        return null;
    }

    // 調査対象を見ている場合、プロンプトを表示する
    private void UpdateInteractionPrompt(Interactable interactable, bool canInteract)
    {
        if (canInteract)
        {
            ShowPrompt(interactable.GetInteractionText());
        }
        else
        {
            HidePrompt();
        }
    }

    // プロンプトを表示する
    public void ShowPrompt(string text)
    {
        interactionPrompt.SetActive(true);
        actionText.text = text;
    }

    // プロンプトを非表示にする
    public void HidePrompt()
    {
        interactionPrompt.SetActive(false);
    }
}
