using UnityEngine;

public abstract class PickUp : MonoBehaviour, IInteractable
{
    public string itemName;
    public Sprite itemSprite; //for UI
    public ItemType RequiredItem => ItemType.None;
    public abstract string InteractionVerb { get; }

    public abstract void Interact();

    public void OnRayHit()
    {
        Debug.Log($"Looking at {itemName}");
        UIManager.Instance.ShowControlHintUI(InteractionVerb);
    }
    protected void DestroyPickup()
    {
        Destroy(gameObject);
    }
}
