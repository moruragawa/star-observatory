using System.Collections;
using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
    [SerializeField] private Transform leftPivot;
    [SerializeField] private Transform rightPivot;
    [SerializeField] private float openAngle = 120f;
    [SerializeField] private float openDuration = 1f;

    private Quaternion leftClosedRotation;
    private Quaternion rightClosedRotation;
    private bool isOpen;
    private bool isAnimating;

    public bool IsOpen => isOpen;

    private void Awake()
    {
        leftClosedRotation = leftPivot.localRotation;
        rightClosedRotation = rightPivot.localRotation;
    }

    public void Open()
    {
        if (isOpen || isAnimating) return;

        StartCoroutine(OpenDoors());
    }

    // キャビネットの扉を開けるアニメーション
    private IEnumerator OpenDoors()
    {
        isAnimating = true;

        Quaternion leftOpenRotation =
            leftClosedRotation * Quaternion.Euler(0f, openAngle, 0f);
        Quaternion rightOpenRotation =
            rightClosedRotation * Quaternion.Euler(0f, -openAngle, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < openDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / openDuration);

            leftPivot.localRotation =Quaternion.Slerp(
                leftClosedRotation, leftOpenRotation, progress);
            rightPivot.localRotation =Quaternion.Slerp(
                rightClosedRotation, rightOpenRotation, progress);

            yield return null;
        }

        leftPivot.localRotation = leftOpenRotation;
        rightPivot.localRotation = rightOpenRotation;

        isOpen = true;
        isAnimating = false;
    }
}
