using UnityEngine;

public class HintViewInteractable : FixedViewInteractable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameManager.HintId hintId;

    public override void Interact()
    {
        // 固定視点に切り替えて、ヒントを確認済みにする
        base.Interact();
        gameManager.MarkHintViewed(hintId);
    }
}
