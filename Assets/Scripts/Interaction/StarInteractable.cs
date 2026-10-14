using UnityEngine;

public class StarInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DeskPaper deskPaper;

    public override void Interact()
    {
        gameManager.GetStar();

        if (gameManager.CurrentState == GameManager.GameState.FindStar1)
        {
            deskPaper.Move();
        }

        gameObject.SetActive(false);
    }
}
