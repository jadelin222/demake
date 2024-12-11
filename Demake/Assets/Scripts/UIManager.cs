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

    [Header("Full Screen UI")]
    public GameObject fullScreenUI;

    [Header("Time UI")]
    [SerializeField] public TMP_Text timeTxt;
    [SerializeField] private GameTime gameTime;

    [Header("Ray Interation UI")]
    public Image uiImage;
    public Sprite defaultSprite;
    public Sprite interactableSprite;

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
    public void ShowFullScreenUI()
    {

    }
    public void ShowPickupUI(string actionVerb, string bottomMessage)
    {
        //show control hint
        colsePanellHintUI.SetActive(true);
        closeHintText.text = $"Q - {actionVerb}";

        pickUpMaskUI.SetActive(true);
        bottomLeftMessage.text = bottomMessage;

        //later for displaying letters/texts/polaroid? 
        //if (blackBackground != null)
        //    blackBackground.SetActive(true);
    }
    public void HidePickupUI()
    {
        HideControlHintUI();       
        pickUpMaskUI.SetActive(false);
        colsePanellHintUI.SetActive(false);

        //if (blackBackground != null)
        //    blackBackground.SetActive(false);
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
    /// interactable item when hit by ray will change the UI in the middle of the screen
    /// </summary>
    public void HideInteractableUI()
    {
        uiImage.sprite = defaultSprite;
    }

    public void ShowInteractableUI()
    {
        uiImage.sprite = interactableSprite;
    }
}
