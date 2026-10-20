using UnityEngine;

public class GearInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private StarSlotDrawer starSlotDrawer;

    [Header("Message")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private string message = "歯車を手に入れた！";
    [SerializeField] private Sprite gearIcon;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    public override void Interact()
    {
        gameManager.GetGear();
        audioManager.PlaySound(AudioManager.SoundType.Pickup);

        gameObject.SetActive(false);

        starSlotDrawer.Close();

        uiManager.ShowMessage(message, gearIcon);
    }
}
