using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private string interactionText = "調べる";

    public virtual bool CanInteract => true;

    public virtual string GetInteractionText()
    {
        return interactionText;
    }

    public virtual void Interact()
    {
        
    }
}
