using UnityEngine;

public class StarSlotInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject placedStar;

    public override void Interact()
    {
        if (!gameManager.HasStar)
        {
            Debug.Log("星を持っていません");
            return;
        }

        // 所持している星を配置してゲーム状態を進める

        placedStar.SetActive(true);

        gameManager.UseStar();

        gameManager.AdvanceState();

        Debug.Log("星を置きました");
    }
}
