using UnityEngine;

public class KeyInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private StarSlotDrawer starSlotDrawer;

    [Header("Message")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private string message = "鍵を手に入れた！";
    [SerializeField] private Sprite keyIcon;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    public override void Interact()
    {
        gameManager.GetKey();
        audioManager.PlaySound(AudioManager.SoundType.Pickup);

        gameObject.SetActive(false);

        starSlotDrawer.Close();

        uiManager.ShowMessage(message,keyIcon);
    }
}
