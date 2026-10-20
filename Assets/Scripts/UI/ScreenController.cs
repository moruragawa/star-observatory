using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject hud;

    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Interactor playerInteractor;

    [Header("Clear")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CanvasGroup fadeOverlay;
    [SerializeField] private float fadeOutDuration = 2.5f;
    [SerializeField] private float fadeInDuration = 1f;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    private bool isShowingClear;

    private void Awake()
    {
        titlePanel.SetActive(true);
        introPanel.SetActive(false);
        clearPanel.SetActive(false);
        hud.SetActive(false);

        playerController.enabled = false;
        playerInteractor.enabled = false;

        fadeOverlay.alpha = 0f;
        fadeOverlay.blocksRaycasts = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // タイトル画面を非表示にし、説明画面を表示する
    public void ShowIntro()
    {
        audioManager.PlaySound(AudioManager.SoundType.Button);

        titlePanel.SetActive(false);
        introPanel.SetActive(true);
    }

    // 説明画面を非表示にし、本編を開始する
    public void StartGame()
    {
        audioManager.PlaySound(AudioManager.SoundType.Button);

        introPanel.SetActive(false);
        hud.SetActive(true);

        playerController.enabled = true;
        playerInteractor.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // クリア画面を表示する
    public void ShowClearScreen()
    {
        if (isShowingClear) return;

        isShowingClear = true;

        playerController.enabled = false;
        playerInteractor.enabled = false;

        StartCoroutine(ShowClearScreenRoutine());
    }

    // クリア画面表示の演出アニメーション
    private IEnumerator ShowClearScreenRoutine()
    {
        // メッセージの表示を待つ
        yield return null;

        // メッセージが消えるまで待つ
        while (uiManager.IsMessagePlaying) yield return null;

        fadeOverlay.blocksRaycasts = true;
        yield return Fade(0f, 1f, fadeOutDuration);

        hud.SetActive(false);
        clearPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return Fade(1f, 0f, fadeInDuration);

        fadeOverlay.blocksRaycasts = false;

        audioManager.PlaySound(AudioManager.SoundType.Clear);
    }

    // 画面のフェードイン・フェードアウト
    private IEnumerator Fade(
        float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        fadeOverlay.alpha = startAlpha;

        if (duration <= 0f)
        {
            fadeOverlay.alpha = targetAlpha;
            yield break;
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(elapsedTime / duration);
            fadeOverlay.alpha = Mathf.Lerp(
                startAlpha, targetAlpha, progress);

            yield return null;
        }

        fadeOverlay.alpha = targetAlpha;
    }

    // タイトル画面に戻る
    public void ReturnToTitle()
    {
        audioManager.PlaySound(AudioManager.SoundType.Button);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
