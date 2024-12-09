using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CameraRayInteracter : MonoBehaviour
{

    [Header("Raycast")]
    RaycastHit rayHit;

    [Range(0, 100)]
    public float maxDist = 1.2f; // m
    public LayerMask interactableLayer;

    [Header("UI")]
    public Image uiImage;
    public Sprite defaultSprite;         
    public Sprite interactableSprite;


    void Update()
    {
        //ray go out from cam to the center of screen and out 
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        //tool controls
        if (Input.GetKeyDown(KeyCode.C) || Input.GetMouseButtonDown(1))  //cycle to the next tool
        {
            ToolSystem.Instance.CycleToNextTool();
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))  
        {
            ToolSystem.Instance.UseActiveTool();
        }

        //switch between different sprites to indicate if obejct is interactable
        if (Physics.Raycast(ray, out rayHit, maxDist, interactableLayer))
        {
            Debug.DrawLine(Camera.main.transform.position, rayHit.point, Color.red, 2f);
            Debug.Log("raycast hit: " + rayHit.collider.name);

            //IInteractable interactable = rayHit.collider.GetComponent<IInteractable>();
            IInteractable interactable = rayHit.collider.GetComponentInParent<IInteractable>();  //detact also the child objects' collider

            if (interactable != null)
            {
                Debug.Log("interactable object hit!");
                uiImage.sprite = interactableSprite;
                interactable.OnRayHit();
                 
                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                {
                    Debug.Log("E pressed");
                    //interactable.Interact();  
                    HandleInteraction(interactable);

                }
            }
        }
        else
        {
            //Debug.DrawLine(Camera.main.transform.position, rayHit.point, Color.green, 2f);//not working
            uiImage.sprite = defaultSprite;
            UIManager.Instance.HideControlHintUI();
        }
    }
    private void HandleInteraction(IInteractable interactable)
    {
        ItemType requiredItem = interactable.RequiredItem;
        //check if the game object in world need certain item to be interacted with. or if it's tool, tool need to be equipped when interacting
        if (requiredItem == ItemType.None || requiredItem == ToolSystem.Instance.EquippedToolType)
        {
            interactable.Interact();
        }
        else
        {
            //td: prompt ui
            Debug.Log($"you need a {requiredItem} to interact with this");
        }
    }
}
