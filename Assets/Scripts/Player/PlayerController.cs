using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 0.07f;
    [SerializeField] private Transform mainCamera;

    private InputSystem_Actions inputActions;
    private CharacterController controller;
    private float cameraPitch = 0f;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        // WASDキーによる移動操作
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // マウスによる視点操作
        Vector2 lookInput = inputActions.Player.Look.ReadValue<Vector2>();

        transform.Rotate(Vector3.up * lookInput.x * lookSensitivity);

        cameraPitch -= lookInput.y * lookSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);

        mainCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }
}
