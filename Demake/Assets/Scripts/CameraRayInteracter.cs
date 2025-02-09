
using UnityEngine;

public class CameraRayInteracter : MonoBehaviour
{

    [Header("Raycast")]
    RaycastHit rayHit;

    [Range(0, 100)]
    public float maxDist = 1.2f; // m
    public LayerMask interactableLayer;

    void Update()
    {
        //ray go out from cam to the center of screen and out 
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        //switch between different sprites to indicate if obejct is interactable
        if (Physics.Raycast(ray, out rayHit, maxDist, interactableLayer))
        {
            //Debug.DrawLine(Camera.main.transform.position, rayHit.point, Color.red, 2f);
            //Debug.Log("raycast hit: " + rayHit.collider.name);

            //IInteractable interactable = rayHit.collider.GetComponent<IInteractable>();
            IInteractable interactable = rayHit.collider.GetComponentInParent<IInteractable>();  //detact also the child objects' collider

            if (interactable != null)
            {
                //Debug.Log("interactable object hit!");
                //uiImage.sprite = interactableSprite;
                UIManager.Instance.ShowInteractableUI();
                interactable.OnRayHit();
                 
                if (Input.GetKeyDown(KeyCode.E))
                    HandleInteraction(interactable);

                if (Input.GetMouseButtonDown(0)) // Left Mouse Button
                    HandleToolUsage(interactable);
            }
        }
        else
        {
            //Debug.DrawLine(Camera.main.transform.position, rayHit.point, Color.green, 2f);//not working
            //uiImage.sprite = defaultSprite;
            //UIManager.Instance.HideInteractableUI();
            UIManager.Instance.ShowNonInteractableUI();
            UIManager.Instance.HideControlHintUI();
        }
    }
    private void HandleInteraction(IInteractable interactable)
    {
        //ItemType requiredItem = interactable.RequiredItem;
        ////if the no item required, or required item is tool and is equipped, or required item is in the ItemSystem inventory
        //if (requiredItem == ItemType.None || requiredItem == ToolSystem.Instance.EquippedToolType || ItemSystem.Instance.HasItem(requiredItem))
        //{
        //    interactable.Interact();
        //}
        //else
        //{
        //    //td: prompt ui
        //}
        interactable.Interact();
    }
    private void HandleToolUsage(IInteractable interactable)
    {
        //check if the interactable requires a tool and matches the currently equipped tool
        if (interactable.RequiredItem == ToolSystem.Instance.EquippedToolType)
        {
            interactable.Interact();
        }
        else if (interactable.RequiredItem != ItemType.None)
        {

            //Debug.Log($"you need a {interactable.RequiredItem} equipped to use this tool.");
            //td: prompt bottom screen ui'
        }
    }
}
