using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using static UnityEngine.Rendering.BoolParameter;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Control Hint UI")]
    public GameObject controlHintUI;
    public TMP_Text controlHintText;

    [Header("Pick Up Mask UI")]
    public GameObject pickUpMaskUI;        //main UI for all pickup UI
    public TMP_Text bottomLeftMessage;     //"Obtained {name}"
    //public GameObject blackBackground;
    public GameObject ItemOrToolUI;
    public GameObject PolaroidUI;
    public GameObject LetterUI;
    public TMP_Text letterText;
    public Image polaroidImage;
    public TMP_Text polaroidText;

    //public GameObject colsePanellHintUI;
    public TMP_Text closeHintText;

    [Header("Bottom Screen UI")]
    public GameObject bottomScreenUI;
    public TMP_Text bottomScreenText;

    [Header("Time UI")]
    [SerializeField] public TMP_Text timeTxt;
    [SerializeField] private GameTime gameTime;

    [Header("Ray Interation Cursor UI")]
    public GameObject CursorUI;
    public Image cursorImage;
    public Sprite defaultSprite;
    public Sprite interactableSprite;

    private Action onPickupClosed;  //callback to finalize pickup


    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (gameTime != null)
        {
            timeTxt.text = gameTime.GetTime();
        }
        //close pick-up UI when Q is pressed
        if (pickUpMaskUI.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            HidePickupUI();
        }

    }
    public void ShowBottomScreenUI()
    {
        //with type writer fx
    }

    /// <summary>
    /// When attempt to pick up item, shows this ui to display info
    /// </summary>
    public void ShowPickupUI(PickupData uiData, Action onClose)
    //public void ShowPickupUI(PickupDisplayType displayType, string bottomMessage, Action onClose)
    {
        ToolSystem.Instance.PutAwayActiveTool();
        pickUpMaskUI.SetActive(true); //bring up the UI group for pickup interaction

        //hide all other panels
        ItemOrToolUI.SetActive(false);
        LetterUI.SetActive(false);
        PolaroidUI.SetActive(false);

        // Decide which UI to show
        switch (uiData.displayType)
        {
            case PickupDisplayType.ItemOrTool:
                ShowItemOrToolUI(uiData);
                break;

            case PickupDisplayType.Polaroid:
                ShowPolaroidUI(uiData);
                break;

            case PickupDisplayType.Letter:
                ShowLetterUI(uiData);
                break;
        }
        Debug.Log("Callback assigned in ShowPickupUI.");
        onPickupClosed = onClose;
        FreezeCamera();

    }
    public void HidePickupUI()
    {
        HideControlHintUI();
        ShowInteractableUI();
        
        pickUpMaskUI.SetActive(false);

        //invoke the callback to finalize pickup
        Debug.Log("Invoking callback in HidePickupUI...");
        onPickupClosed?.Invoke();
        onPickupClosed = null;
        Debug.Log("Callback executed.");

        ResumeCamera();
    }
    public void ShowItemOrToolUI(PickupData uiData)
    {
        ItemOrToolUI.SetActive(true);
        bottomLeftMessage.text = uiData.bottomMessage;
        closeHintText.text = $"Q - {uiData.actionVerb}";

    }
    public void ShowLetterUI(PickupData uiData)
    {
        LetterUI.SetActive(true);
        bottomLeftMessage.text = uiData.bottomMessage;
        letterText.text = uiData.descriptionText;
        closeHintText.text = $"Q - {uiData.actionVerb}";

    }
    public void ShowPolaroidUI(PickupData uiData)
    {
        PolaroidUI.SetActive(true);
        bottomLeftMessage.text = uiData.bottomMessage;
        closeHintText.text = $"Q - {uiData.actionVerb}";

        polaroidImage.sprite = uiData.imageSprite;
        polaroidText.text = uiData.descriptionText;
    }
    /// <summary>
    /// the control hint shown when raycast hit interactable object
    /// </summary>
    public void ShowControlHintUI(string actionVerb)
    {
        controlHintUI.SetActive(true);
        controlHintText.text = $"E - {actionVerb}";
    }
    public void HideControlHintUI()
    {
        controlHintUI.SetActive(false);
    }

    /// <summary>
    /// MID SCREEN CURSOR - interactable item when hit by ray will change the cursor UI at the center of the screen
    /// </summary>
    public void HideInteractableUI()
    {
        CursorUI.SetActive(false);
    }
    public void ShowNonInteractableUI()
    {
        cursorImage.sprite = defaultSprite;
    }
    public void ShowInteractableUI()
    {
        cursorImage.sprite = interactableSprite;
    }

    /// <summary>
    /// freeze camera movement by freezing cursor, also hide the cursor point in the middle of screen, freeze time. 
    /// </summary>
    private void FreezeCamera()
    {
        CursorUI.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }
    //resume camera movement
    private void ResumeCamera()
    {
        CursorUI.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
