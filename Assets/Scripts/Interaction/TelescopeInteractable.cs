using UnityEngine;

public class TelescopeInteractable : Interactable
{
    [SerializeField] private CameraManager cameraManager;

    public override void Interact()
    {
        // 望遠鏡視点に切り替える
        cameraManager.SwitchToTelescope();
    }
}
