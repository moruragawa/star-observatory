using UnityEngine;

public class GearSlotInteractable : Interactable
{
    [Header("Game State")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject placedGear;

    [Header("Message")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private string gearMissingMessage = "何かを取り付けられそうだ";
    [SerializeField] private string gearPlacedMessage = "歯車を取り付けた！望遠鏡が使えるかも";
    [SerializeField] private string gearAlreadyPlacedMessage = "歯車はしっかりはまっている";

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
        // すでに歯車が取り付けられている場合
        if (placedGear.activeSelf)
        {
            uiManager.ShowMessage(gearAlreadyPlacedMessage);
            return;
        }

        // 歯車を持っていない場合
        if (!gameManager.HasGear)
        {
            uiManager.ShowMessage(gearMissingMessage);
            return;
        }

        gameManager.UseGear();
        placedGear.SetActive(true);
        gameManager.UnlockTelescope();

        uiManager.ShowMessage(gearPlacedMessage);
    }
}
