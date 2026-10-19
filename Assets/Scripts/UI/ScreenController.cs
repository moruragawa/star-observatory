using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject hud;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        titlePanel.SetActive(true);
        introPanel.SetActive(false);
        hud.SetActive(false);

        playerController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // タイトル画面を非表示にし、説明画面を表示する
    public void ShowIntro()
    {
        titlePanel.SetActive(false);
        introPanel.SetActive(true);
    }

    // 説明画面を非表示にし、本編を開始する
    public void StartGame()
    {
        introPanel.SetActive(false);
        hud.SetActive(true);

        playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
