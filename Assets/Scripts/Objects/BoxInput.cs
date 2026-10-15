using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxInput : MonoBehaviour
{
    [Header("Number Display")]
    [SerializeField] private TMP_Text[] numberTexts;

    [Header("Markers")]
    [SerializeField] private RectTransform upperMarker;
    [SerializeField] private RectTransform lowerMarker;
    [SerializeField] private Transform[] upperPoints;
    [SerializeField] private Transform[] lowerPoints;

    [Header("Answer")]
    [SerializeField] private string correctAnswer = "635";

    [Header("Box")]
    [SerializeField] private BoxViewInteractable boxViewInteractable;
    [SerializeField] private GameManager gameManager;

    [Header("UI")]
    [SerializeField] private GameObject dot;

    public bool IsInputActive { get; private set; }

    private InputSystem_Actions inputActions;
    private int selectedDigit;
    private int[] digits = { 0, 0, 0 };

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        UpdateNumberDisplay();
        UpdateMarkerPositions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Disable();
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }

    private void Update()
    {
        if (!IsInputActive) return;

        // Eキーで正誤判定をする
        if (inputActions.Player.Interact.WasPressedThisFrame())
        {
            ConfirmInput();
        }
    }

    // 数字入力を開始する
    public void BeginInput()
    {
        IsInputActive = true;
        UpdateMarkerPositions();

        upperMarker.gameObject.SetActive(true);
        lowerMarker.gameObject.SetActive(true);

        dot.SetActive(false);
    }

    // 数字入力を終了する
    public void EndInput()
    {
        IsInputActive = false;

        upperMarker.gameObject.SetActive(false);
        lowerMarker.gameObject.SetActive(false);

        dot.SetActive(true);
    }

    // W/Sキーで数字を変更し、A/Dキーで桁を移動する
    private void OnMove(InputAction.CallbackContext context)
    {
        if (!IsInputActive) return;

        Vector2 moveInput = context.ReadValue<Vector2>();

        if (moveInput.y > 0.5f) ChangeDigit(1);
        else if (moveInput.y < -0.5f) ChangeDigit(-1);
        else if (moveInput.x > 0.5f) MoveSelection(1);
        else if (moveInput.x < -0.5f) MoveSelection(-1);
    }

    // 入力された数字の正誤判定をする
    private void ConfirmInput()
    {
        string inputNumber = string.Join("", digits);

        if (inputNumber != correctAnswer)
        {
            Debug.Log("数字が間違っています");
            return;
        }

        if (!gameManager.HasViewedAllHints())
        {
            Debug.Log("まだ見つけていない手がかりがあります");
            return;
        }

        Debug.Log("正解です");
        EndInput();
        boxViewInteractable.OpenBox();
    }

    // 選択中の桁の数字を変更する
    private void ChangeDigit(int amount)
    {
        digits[selectedDigit] += amount;

        if (digits[selectedDigit] > 9) digits[selectedDigit] = 0;
        else if (digits[selectedDigit] < 0) digits[selectedDigit] = 9;

        UpdateNumberDisplay();
    }

    // 選択する桁を変更する
    private void MoveSelection(int direction)
    {
        selectedDigit += direction;

        if (selectedDigit > 2) selectedDigit = 0;
        else if (selectedDigit < 0) selectedDigit = 2;

        UpdateMarkerPositions();
    }

    // 数字の表示を更新する
    private void UpdateNumberDisplay()
    {
        for (int i = 0; i < 3; i++)
        {
            numberTexts[i].text = digits[i].ToString();
        }
    }

    // 選択中の桁に合わせてマーカーを移動する
    private void UpdateMarkerPositions()
    {
        if (upperMarker == null || lowerMarker == null) return;

        if (upperPoints.Length != 3 || lowerPoints.Length != 3) return;

        upperMarker.position = upperPoints[selectedDigit].position;
        lowerMarker.position = lowerPoints[selectedDigit].position;
    }
}
