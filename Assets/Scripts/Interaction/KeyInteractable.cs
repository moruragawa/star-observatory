using UnityEngine;

public class KeyInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private StarSlotDrawer starSlotDrawer;

    public override void Interact()
    {
        gameManager.GetKey();

        gameObject.SetActive(false);

        starSlotDrawer.Close();
    }
}
