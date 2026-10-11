using UnityEngine;

public class GearInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private StarSlotDrawer starSlotDrawer;

    public override void Interact()
    {
        gameManager.GetGear();

        gameObject.SetActive(false);

        starSlotDrawer.Close();
    }
}
