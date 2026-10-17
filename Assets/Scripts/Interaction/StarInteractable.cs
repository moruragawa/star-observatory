using UnityEngine;

public class StarInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DeskPaper deskPaper;
    [SerializeField, Range(0, 2)] private int starIndex;

    public override void Interact()
    {
        gameManager.GetStar(starIndex);

        if (starIndex == 0)
        {
            deskPaper.Move();
        }

        gameObject.SetActive(false);
    }
}
