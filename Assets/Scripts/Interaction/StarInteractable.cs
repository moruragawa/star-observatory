using UnityEngine;

public class StarInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DeskPaper deskPaper;
    [SerializeField, Range(0, 2)] private int starIndex;

    [Header("Message")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private string message = "星を手に入れた！";
    [SerializeField] private Sprite[] starIcons = new Sprite[3];

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    public override void Interact()
    {
        gameManager.GetStar(starIndex);
        audioManager.PlaySound(AudioManager.SoundType.Pickup);

        if (starIndex == 0)
        {
            deskPaper.Move();
        }

        gameObject.SetActive(false);

        Sprite icon = starIndex < starIcons.Length ? starIcons[starIndex] : null;
        uiManager.ShowMessage(message, icon);
    }
}
