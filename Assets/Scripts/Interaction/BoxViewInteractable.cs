using UnityEngine;

public class BoxViewInteractable : FixedViewInteractable
{
    [SerializeField] private BoxLid boxLid;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("箱を見ました");

        boxLid.Open();
    }
}
