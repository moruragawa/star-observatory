using UnityEngine;

public class FixedViewInteractable : Interactable
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Transform viewPoint;

    public override void Interact()
    {
        cameraManager.SwitchToFixed(viewPoint);
    }
}
