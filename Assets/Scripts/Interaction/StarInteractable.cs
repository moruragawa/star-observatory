using UnityEngine;

public class StarInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;

    public override void Interact()
    {
        gameManager.GetStar();

        gameObject.SetActive(false);
    }
}
