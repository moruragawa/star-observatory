using System.Collections;
using UnityEngine;

public class BoxViewInteractable : FixedViewInteractable
{
    [SerializeField] private BoxLid boxLid;
    [SerializeField] private Collider interactionCollider;
    [SerializeField] private StarInteractable starInteractable;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("箱を見ました");

        StartCoroutine(OpenBox());
    }

    private IEnumerator OpenBox()
    {
        boxLid.Open();

        // ふたが開き終わるまで待つ
        yield return new WaitUntil(() => boxLid.IsOpen);

        // 箱のインタラクションを無効にし、星2のインタラクションを有効にする
        interactionCollider.enabled = false;
        starInteractable.enabled = true;

        enabled = false;
    }
}
