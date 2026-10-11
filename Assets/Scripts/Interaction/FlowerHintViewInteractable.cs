using UnityEngine;

public class FlowerHintViewInteractable : FixedViewInteractable
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("花のヒントを確認しました");
    }
}
