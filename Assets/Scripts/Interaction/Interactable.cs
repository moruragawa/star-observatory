using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private string interactionText = "調べる";

    public string InteractionText => interactionText;

    public virtual void Interact()
    {
        
    }
}
