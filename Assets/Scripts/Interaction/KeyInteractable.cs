using UnityEngine;

public class KeyInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private StarSlotDrawer starSlotDrawer;

    [Header("Message")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private string message = "鍵を手に入れた！";
    [SerializeField] private Sprite keyIcon;

    public override void Interact()
    {
        gameManager.GetKey();

        gameObject.SetActive(false);

        starSlotDrawer.Close();

        uiManager.ShowMessage(message,keyIcon);
    }
}
