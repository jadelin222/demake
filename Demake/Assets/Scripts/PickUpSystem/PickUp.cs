using UnityEngine;

public abstract class PickUp : MonoBehaviour, IInteractable
{
    public string itemName;
    public Sprite itemSprite; //for UI
    public ItemType RequiredItem => ItemType.None;

    public abstract void Interact();

    public void OnRayHit()
    {
        Debug.Log($"Looking at {itemName}");
        //UIManager.Instance.ShowControlHint($"Press E to pick up {itemName}");
    }
    protected void DestroyPickup()
    {
        Destroy(gameObject);
    }
}
