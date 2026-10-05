using UnityEngine;

public class StarInteractable : Interactable
{
    public override void Interact()
    {
        gameObject.SetActive(false);
    }
}
