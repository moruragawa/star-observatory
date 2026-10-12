using UnityEngine;

public class GearSlotInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject placedGear;

    // 歯車の所持状態によってプロンプト表示を切り替える
    public override string GetInteractionText()
    {
        if (gameManager.HasGear)
        {
            return "歯車を置く";
        }

        return "調べる";
    }

    public override void Interact()
    {
        if (gameManager.HasGear)
        {
            gameManager.UseGear();
            placedGear.SetActive(true);

            Debug.Log("歯車を取り付けました");
        }
    }
}
