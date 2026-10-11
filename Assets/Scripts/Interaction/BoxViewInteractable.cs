using System.Collections;
using UnityEngine;

public class BoxViewInteractable : FixedViewInteractable
{
    [SerializeField] private BoxLid boxLid;
    [SerializeField] private StarInteractable starInteractable;
    [SerializeField] private Collider interactionCollider;

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

        // 星2のインタラクションを有効にする
        interactionCollider.enabled = false;

        // 箱本体のインタラクションを無効にする
        starInteractable.enabled = true;
        enabled = false;
    }
}
