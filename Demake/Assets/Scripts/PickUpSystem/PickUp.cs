using UnityEngine;

public abstract class PickUp : MonoBehaviour, IInteractable
{
    public string itemName;
    public PickupDisplayType displayType;
    public string descriptionText;
    public Sprite image;
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
        UIManager.Instance.ShowControlHintUI(InteractionVerb);
    }

}
