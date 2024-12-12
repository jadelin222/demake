using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Control Hint UI")]
    public GameObject controlHintUI;
    public TMP_Text controlHintText;

    [Header("Pick Up Mask UI")]
    public GameObject pickUpMaskUI;        //main UI for real-time masked display
    public TMP_Text bottomLeftMessage;     //"Obtained {name}"
    public GameObject blackBackground;

    public GameObject colsePanellHintUI;
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
    private GameObject previousActiveTool; //store the reference to the equipped tool



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
    //public void ShowPickupUI(string actionVerb, string bottomMessage)
    public void ShowPickupUI(string actionVerb, string bottomMessage, Action onClose)
    {

        previousActiveTool = ToolSystem.Instance.GetActiveToolObject();
        if (previousActiveTool != null)
        {
            previousActiveTool.SetActive(false);
        }
        //show control hint
        CursorUI.SetActive(false);
        colsePanellHintUI.SetActive(true);
        closeHintText.text = $"Q - {actionVerb}";

        pickUpMaskUI.SetActive(true);
        bottomLeftMessage.text = bottomMessage;

        onPickupClosed = onClose;

        FreezeCamera();

        //later for displaying letters/texts/polaroid? 
        //if (blackBackground != null)
        //    blackBackground.SetActive(true);
    }
    public void HidePickupUI()
    {

        HideControlHintUI();
        CursorUI.SetActive(true);
        pickUpMaskUI.SetActive(false);
        colsePanellHintUI.SetActive(false);

        //invoke the callback to finalize pickup
        onPickupClosed?.Invoke();
        onPickupClosed = null;
        ResumeCamera();


        //if (blackBackground != null)
        //    blackBackground.SetActive(false);
    }
    public void ShowLetterUI()
    {

    }
    public void ShowPolaroidUI()
    {

    }
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
    /// interactable item when hit by ray will change the cursor UI at the center of the screen
    /// </summary>
    public void HideInteractableUI()
    {
        cursorImage.sprite = defaultSprite;
    }
    public void ShowInteractableUI()
    {
        cursorImage.sprite = interactableSprite;
    }

    //freeze camera movement by freezing cursor
    private void FreezeCamera()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    //resume camera movement
    private void ResumeCamera()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
