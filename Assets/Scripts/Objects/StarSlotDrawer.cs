using System.Collections;
using UnityEngine;

public class StarSlotDrawer : MonoBehaviour
{
    [SerializeField] private float moveDistance = 0.45f;
    [SerializeField] private float animationDuration = 0.5f;

    private Vector3 closedPosition;
    private bool isOpen;
    private bool isAnimating;

    private void Awake()
    {
        closedPosition = transform.localPosition;
    }

    public void Open()
    {
        if (isOpen || isAnimating) return;

        Vector3 openPosition =
            closedPosition + Vector3.back * moveDistance;
        
        StartCoroutine(MoveDrawer(openPosition, true));
    }

    public void Close()
    {
        if (!isOpen || isAnimating) return;

        StartCoroutine(MoveDrawer(closedPosition, false));
    }

    // 星置き場の引き出しを開閉するアニメーション
    private IEnumerator MoveDrawer(Vector3 targetPosition, bool opening)
    {
        isAnimating = true;

        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsedTime / animationDuration);

            transform.localPosition = Vector3.Lerp(
                startPosition, targetPosition, progress);
            
            yield return null;
        }

        transform.localPosition = targetPosition;
        isOpen = opening;
        isAnimating =false;
    }
}
