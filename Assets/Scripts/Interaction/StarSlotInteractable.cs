using UnityEngine;

public class StarSlotInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject[] placedStars;
    [SerializeField] private StarSlotDrawer starSlotDrawer;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject gear;

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
        gameManager.UseStar();

        // ゲーム状態を更新する
        gameManager.AdvanceState();

        Debug.Log("星を置きました");
    }
}
