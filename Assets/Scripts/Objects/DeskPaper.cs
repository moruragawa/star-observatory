using System.Collections;
using UnityEngine;

public class DeskPaper : MonoBehaviour
{
    [SerializeField] private float moveDistance = 0.2f;
    [SerializeField] private float animationDuration = 0.5f;

    private Vector3 initialPosition;
    private bool hasMoved;
    private bool isAnimating;

    public void Awake()
    {
        initialPosition = transform.localPosition;
    }

    public void Move()
    {
        if (hasMoved || isAnimating) return;

        Vector3 targetPosition =
            initialPosition + Vector3.back * moveDistance;

        StartCoroutine(MovePaper(targetPosition));
    }

    // 紙を動かすアニメーション
    private IEnumerator MovePaper(Vector3 targetPosition)
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
        hasMoved = true;
        isAnimating = false;
    }
}
