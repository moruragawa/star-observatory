using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Stars")]
    [SerializeField] private Image[] starIcons;
    [SerializeField] private Sprite[] coloredStarSprites;
    [SerializeField] private GameObject[] starCheckMarks;

    [Header("Items")]
    [SerializeField] private GameObject keyIcon;
    [SerializeField] private GameObject gearIcon;

    [Header("Animation")]
    [SerializeField] private float popScale = 1.2f;
    [SerializeField] private float popDuration = 0.2f;

    [Header("Message")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Image messageIcon;
    [SerializeField] private TMPro.TMP_Text messageText;
    [SerializeField] private CanvasGroup messageCanvasGroup;
    [SerializeField] private float messageDuration = 2f;
    [SerializeField] private float messageDelay = 0.2f;
    [SerializeField] private float fadeDuration = 0.2f;

    private Coroutine messageCoroutine;

    public bool IsMessagePlaying => messageCoroutine != null;

    // 星を取得したとき、アイコンをカラー画像に切り替える
    public void UpdateStarCollected(int index)
    {
        starIcons[index].sprite = coloredStarSprites[index];
        StartCoroutine(PlayPopAnimation(starIcons[index].rectTransform));
    }

    // 星を配置したとき、アイコンにチェックマークを表示する
    public void UpdateStarPlaced(int index)
    {
        starCheckMarks[index].SetActive(true);

        RectTransform checkMark =
            starCheckMarks[index].GetComponent<RectTransform>();
        
        StartCoroutine(PlayPopAnimation(checkMark));
    }

    // 鍵アイコンの表示を切り替える
    public void ShowKeyIcon(bool visible)
    {
        keyIcon.SetActive(visible);

        if (visible)
        {
            RectTransform icon = keyIcon.GetComponent<RectTransform>();
            StartCoroutine(PlayPopAnimation(icon));
        }
    }

    // 歯車アイコンの表示を切り替える
    public void ShowGearIcon(bool visible)
    {
        gearIcon.SetActive(visible);

        if (visible)
        {
            RectTransform icon = gearIcon.GetComponent<RectTransform>();
            StartCoroutine(PlayPopAnimation(icon));
        }
    }

    // アイコン更新時の演出アニメーション
    private IEnumerator PlayPopAnimation(RectTransform target)
    {
        Vector3 originalScale = target.localScale;
        Vector3 enlargedScale = originalScale * popScale;
        float halfDuration = popDuration / 2f;
        float elapsedTime = 0f;

        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsedTime / halfDuration);
            target.localScale = Vector3.Lerp(originalScale, enlargedScale, t);
            yield return null;
        }

        elapsedTime = 0f;

        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsedTime / halfDuration);
            target.localScale = Vector3.Lerp(enlargedScale, originalScale, t);
            yield return null;
        }

        target.localScale = originalScale;
    }

    // メッセージを表示する
    public void ShowMessage(string message, Sprite icon = null)
    {
        messageText.text = message;

        // アイコンの指定がある場合は表示する
        bool hasIcon = icon != null;
        messageIcon.sprite = icon;
        messageIcon.gameObject.SetActive(hasIcon);

        // 他のメッセージを表示している場合は表示時間をリセットする
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        messagePanel.SetActive(false);
        messageCanvasGroup.alpha = 0f;

        messageCoroutine = StartCoroutine(ShowMessageRoutine());
    }

    // メッセージ表示の演出アニメーション
    private IEnumerator ShowMessageRoutine()
    {
        // 待機後にフェードイン
        yield return new WaitForSeconds(messageDelay);

        messagePanel.SetActive(true);

        yield return FadeMessage(0f, 1f);

        // 表示時間
        yield return new WaitForSeconds(messageDuration);

        // フェードアウト
        yield return FadeMessage(1f, 0f);

        messagePanel.SetActive(false);
        messageCoroutine = null;
    }

    // メッセージのフェードイン・フェードアウト
    private IEnumerator FadeMessage(float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;
        messageCanvasGroup.alpha = startAlpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsedTime / fadeDuration);
            messageCanvasGroup.alpha =
                Mathf.Lerp(startAlpha, targetAlpha, progress);

            yield return null;
        }

        messageCanvasGroup.alpha = targetAlpha;
    }
}
