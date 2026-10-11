using System.Collections;
using UnityEngine;

public class BoxLid : MonoBehaviour
{
    [SerializeField] private Transform boxPivot;
    [SerializeField] private float openAngle = 120f;
    [SerializeField] private float openDuration = 1f;

    private Quaternion closedRotation;
    private bool isOpen;
    private bool isAnimating;

    public bool IsOpen => isOpen;

    private void Awake()
    {
        closedRotation = boxPivot.localRotation;
    }

    public void Open()
    {
        if (isOpen || isAnimating) return;

        StartCoroutine(OpenLid());
    }

    // 箱のふたを開けるアニメーション
    private IEnumerator OpenLid()
    {
        isAnimating = true;

        Quaternion openRotation =
            closedRotation * Quaternion.Euler(-openAngle, 0f, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < openDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / openDuration);

            boxPivot.localRotation = Quaternion.Slerp(
                closedRotation, openRotation, progress);

            yield return null;
        }

        boxPivot.localRotation = openRotation;

        isOpen = true;
        isAnimating = false;
    }
}
