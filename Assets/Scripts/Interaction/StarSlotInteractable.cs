using UnityEngine;

public class StarSlotInteractable : Interactable
{
    [Header("Game")]
    [SerializeField] private GameManager gameManager;

    [Header("Star Slots")]
    [SerializeField] private GameObject[] placedStars;
    [SerializeField] private StarSlotDrawer starSlotDrawer;

    [Header("Items")]
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject gear;

    [Header("UI")]
    [SerializeField] private UIManager uiManager;

    [Header("Message")]
    [SerializeField] private string placedStarMessage = "星を置いた！";
    [SerializeField] private string noStarMessage = "星を置く場所のようだ";
    [SerializeField] private Sprite[] starIcons;

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
            uiManager.ShowMessage(noStarMessage);
            return;
        }

        // 現在のゲーム状態に応じて処理を分ける
        int starIndex;

        switch (gameManager.CurrentState)
        {
            case GameManager.GameState.FindStar1:
                starIndex = 0;

                // 引き出しを開けて鍵を表示する
                starSlotDrawer.Open();
                key.SetActive(true);
                break;
            
            case GameManager.GameState.FindStar2:
                starIndex = 1;

                // 引き出しを開けて歯車を表示する
                starSlotDrawer.Open();
                gear.SetActive(true);
                break;
            
            case GameManager.GameState.FindStar3:
                starIndex = 2;
                break;
            
            default:
                Debug.LogWarning("星を配置できません");
                return;
        }

        // 所持している星を配置する
        placedStars[starIndex].SetActive(true);
        uiManager.UpdateStarPlaced(starIndex);
        gameManager.UseStar();

        // ゲーム状態を更新する
        gameManager.AdvanceState();

        Sprite icon = starIndex < starIcons.Length ? starIcons[starIndex] : null;
        uiManager.ShowMessage(placedStarMessage, icon);
    }
}
