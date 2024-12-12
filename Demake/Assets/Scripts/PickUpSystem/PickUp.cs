using UnityEngine;

public abstract class PickUp : MonoBehaviour, IInteractable
{
    public string itemName;
    public PickupDisplayType displayType;
    public string descriptionText;
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
    //protected void ShowPickupUI()
    //{
    //    UIManager.Instance.ShowPickupUI("Close", $"Obtained {itemName}");
    //    // Use Invoke to finalize pickup when the UI closes
    //    Invoke("FinalizePickup", 0f); // Delayed execution when UI closes
    //}

    //protected void ShowPickupUI()
    //{
    //    switch (displayType)
    //    {
    //        case PickupDisplayType.ItemOrTool:
    //            UIManager.Instance.ShowPickupUI("Close", $"Obtained {itemName}");
    //            break;

    //        case PickupDisplayType.Polaroid:
    //            UIManager.Instance.ShowPolaroidUI();
    //            break;

    //        case PickupDisplayType.Letter:
    //            UIManager.Instance.ShowLetterUI();
    //            break;
    //    }
    //}
    protected void DestroyPickup()
    {
        Destroy(gameObject);
    }
}
