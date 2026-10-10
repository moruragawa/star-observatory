using UnityEngine;

public class StarSlotInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject placedStar;
    [SerializeField] private StarSlotDrawer starSlotDrawer;
    [SerializeField] private GameObject key;

    // 星の所持状態によってプロンプト表示を切り替える
    public override string GetInteractionText()
    {
        if (gameManager.HasStar)
        {
            return "星を置く";
        }

        return "調べる";
    }

    public override void Interact()
    {
        if (!gameManager.HasStar)
        {
            Debug.Log("星を持っていません");
            return;
        }

        // 所持している星を配置する
        placedStar.SetActive(true);
        gameManager.UseStar();

        // 引き出しが開き鍵が出現する
        starSlotDrawer.Open();
        key.SetActive(true);

        // ゲーム状態を更新する
        gameManager.AdvanceState();

        Debug.Log("星を置きました");
    }
}
