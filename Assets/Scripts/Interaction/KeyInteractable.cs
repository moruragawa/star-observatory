using UnityEngine;

public class KeyInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;

    public override void Interact()
    {
        gameManager.GetKey();

        gameObject.SetActive(false);
    }
}
