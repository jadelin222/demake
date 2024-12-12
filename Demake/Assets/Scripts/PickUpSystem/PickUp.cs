using UnityEngine;

public abstract class PickUp : MonoBehaviour, IInteractable
{
    public string itemName;
    public PickupDisplayType displayType;
    public string descriptionText;
    public Sprite polaroidImage;
    public ItemType RequiredItem => ItemType.None;

    public abstract string InteractionVerb { get; }
    public abstract void Interact();
    public virtual void FinalizePickup()
    {
        //destroy the pickup object
        Destroy(gameObject);
    }
    public void OnRayHit()
    {
        Debug.Log($"Looking at {itemName}");
        UIManager.Instance.ShowControlHintUI(InteractionVerb);
    }

}
