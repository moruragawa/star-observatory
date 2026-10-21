using System.Collections;
using UnityEngine;

public class CabinetInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CabinetDoor cabinetDoor;
    [SerializeField] private GameObject insertedKey;
    [SerializeField] private GameObject cabinetLock;
    [SerializeField] private float unlockDelay = 1f;
    [SerializeField] private Collider interactionCollider;
    [SerializeField] private Collider flowerHintCollider;

    [Header("Message")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private string lockedMessage = "鍵がかかっている";
    [SerializeField] private string unlockedMessage = "キャビネットが開いた！";

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    private bool isUnlocked;
    private bool isUnlocking;

    // 解錠中と解錠後は操作できない
    public override bool CanInteract => !isUnlocked && !isUnlocking;

    // 鍵の所持状態によってプロンプト表示を切り替える
    public override string GetInteractionText()
    {
        if (gameManager.HasKey)
        {
            return "開ける";
        }

        return "調べる";
    }

    public override void Interact()
    {
        if (isUnlocked || isUnlocking) return;

        if (!gameManager.HasKey)
        {
            uiManager.ShowMessage(lockedMessage);
            return;
        }

        gameManager.UseKey();

        // 解錠演出を開始
        StartCoroutine(UnlockCabinet());
    }

    private IEnumerator UnlockCabinet()
    {
        isUnlocking = true;

        insertedKey.SetActive(true);

        yield return new WaitForSeconds(unlockDelay);

        insertedKey.SetActive(false);
        cabinetLock.SetActive(false);

        cabinetDoor.Open();
        audioManager.PlaySound(AudioManager.SoundType.UseKey);

        // 扉が開き終わるまで待つ
        yield return new WaitUntil(() => cabinetDoor.IsOpen);

        // キャビネットのインタラクションを無効にし、花のヒントのインタラクションを有効にする
        interactionCollider.enabled = false;
        flowerHintCollider.enabled = true;

        isUnlocked = true;
        isUnlocking =false;

        enabled = false;

        uiManager.ShowMessage(unlockedMessage);
    }
}
