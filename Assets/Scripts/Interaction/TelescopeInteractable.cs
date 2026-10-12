using UnityEngine;

public class TelescopeInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CameraManager cameraManager;

    public override void Interact()
    {
        if (!gameManager.IsTelescopeUsable)
        {
            Debug.Log("望遠鏡はまだ使用できません");
            return;
        }

        // 望遠鏡視点に切り替える
        cameraManager.SwitchToTelescope();
    }
}
