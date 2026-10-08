using UnityEngine;

public class StarSlotDrawer : MonoBehaviour
{
    [SerializeField] private float moveDistance = 0.45f;

    private Vector3 closedPosition;
    private bool isOpen;

    private void Awake()
    {
        closedPosition = transform.localPosition;
    }

    public void Open()
    {
        if (isOpen) return;

        transform.localPosition = closedPosition + Vector3.forward * -moveDistance;
        isOpen = true;
    }

    public void Close()
    {
        if (!isOpen) return;

        transform.localPosition = closedPosition;
        isOpen = false;
    }
}
