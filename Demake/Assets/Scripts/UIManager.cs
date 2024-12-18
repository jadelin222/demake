using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Control Hint UI")]
    public GameObject controlHintUI;
    public TMP_Text controlHintText;

    [Header("Pick Up Mask UI")]
    public GameObject pickUpMaskUI;        //main UI for all pickup UI
    public TMP_Text bottomLeftMessage;     //"Obtained {name}"
    public GameObject ItemOrToolUI;
    public GameObject PolaroidUI;
    public GameObject LetterUI;
    public TMP_Text letterText;
    public Image polaroidImage;
    public TMP_Text polaroidText;

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

    [Header("Inventory UI")]
    public GameObject inventoryUI;
    public TMP_Text categoryTitle;
    public GameObject displayPolaroid;
    public GameObject displayObjOrTool;
    public TMP_Text objOrToolDescription;
    public Image objOrToolImage;
    public TMP_Text polaroidDescription;
    public Image polaroidInventoryImage;
    public Transform textListParent; 
    public GameObject textTemplate;

    [Header("Progress UI")]
    public GameObject progressUI;

    public List<PolaroidData> polaroidCollection = new List<PolaroidData>();
    public List<ItemData> itemCollection = new List<ItemData>();
    public List<ToolData> toolInventory = new List<ToolData>();
    private List<GameObject> textEntries = new List<GameObject>();
    public enum InventoryCategory { Polaroids, Items, Tools }

    private int selectedIndex = 0;
    
    private InventoryCategory currentCategory;
    private bool isInventoryOpen = false;

    private Action onPickupClosed;  //callback to finalize pickup
    private Coroutine typewriterCoroutine;
    private float autoHideDelay = 2f; //typewriter


    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        //td: update current category according to last pickup 

        if (gameTime != null)
        {
            timeTxt.text = gameTime.GetTime();
        }
        if (Input.GetKeyDown(KeyCode.Tab) &&!isInventoryOpen)
        {
                ShowInventory();
        }
        if (isInventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                CloseInventory();

            if (Input.GetKeyDown(KeyCode.RightArrow))
                SwitchToNextCategory();

            if (Input.GetKeyDown(KeyCode.UpArrow))
                MoveSelection(-1);

            if (Input.GetKeyDown(KeyCode.DownArrow))
                MoveSelection(1);
        }
        //close pick-up UI when Q is pressed
        if (pickUpMaskUI.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            HidePickupUI();
        }

    }
    /// <summary>
    /// bottom screenui and type writer fx
    /// </summary>
    public void ShowBottomScreenUI(string message)
    {
        // Stop previous typewriter effect if running
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        // Show the UI and start the typewriter effect
        bottomScreenUI.SetActive(true);
        bottomScreenText.text = "";  // Clear existing text
        typewriterCoroutine = StartCoroutine(TypewriterEffect(message));
    }
    private IEnumerator TypewriterEffect(string message)
    {
        //display the text character-by-character
        foreach (char letter in message)
        {
            bottomScreenText.text += letter;
            yield return new WaitForSeconds(0.05f); // Delay between characters
        }

        //wait for the specified delay before disabling the UI
        yield return new WaitForSeconds(autoHideDelay);
        bottomScreenUI.SetActive(false);
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
    
    public void ShowProgressUI()
    {
        progressUI.SetActive(true); 
    }
    public void HideProgressUI()
    {
        progressUI.SetActive(false);
    }
    /// <summary>
    /// inventory UI
    /// </summary>
    public void ShowInventory()
    {
        if (GetCategoryItemCount(InventoryCategory.Polaroids) == 0 &&
             GetCategoryItemCount(InventoryCategory.Items) == 0 &&
             GetCategoryItemCount(InventoryCategory.Tools) == 0) return;

        FreezeCamera(); 
        inventoryUI.SetActive(true);
        isInventoryOpen = true;
        PopulateCategory(FindFirstNonEmptyCategory());
        //PopulateCategory(lastCategory);
    }
    public void CloseInventory()
    {
        ResumeCamera();
        inventoryUI.SetActive(false);

        isInventoryOpen = false;
    }
    private void PopulateCategory(InventoryCategory category)
    {
        currentCategory = category;
        int itemCount = GetCategoryItemCount(category);
        if (itemCount == 0) return;

        //update the category title
        categoryTitle.text = category.ToString() + "  >";

        //toggle the correct display
        switch (category)
        {
            case InventoryCategory.Polaroids:
                displayPolaroid.SetActive(true);
                displayObjOrTool.SetActive(false);
                break;

            case InventoryCategory.Items:
            case InventoryCategory.Tools:
                displayPolaroid.SetActive(false);
                displayObjOrTool.SetActive(true);
                break;
        }

        PopulateTextList();
    }
    private int GetCategoryItemCount(InventoryCategory category)
    {
        switch (category)
        {
            case InventoryCategory.Polaroids:
                return PolaroidSystem.Instance.GetPolaroidCollection().Count;

            case InventoryCategory.Items:
                return ItemSystem.Instance.GetItemCollection().Count;

            case InventoryCategory.Tools:
                return ToolSystem.Instance.GetToolCollection().Count;

            default:
                return 0;
        }
    }
    private InventoryCategory FindFirstNonEmptyCategory()
    {
        if (GetCategoryItemCount(InventoryCategory.Tools) > 0)
            return InventoryCategory.Tools;
        if (GetCategoryItemCount(InventoryCategory.Polaroids) > 0)
            return InventoryCategory.Polaroids;
        if (GetCategoryItemCount(InventoryCategory.Items) > 0)
            return InventoryCategory.Items;

        return InventoryCategory.Tools; 
    }
    private void PopulateTextList()
    {
        //clear old text entries
        foreach (var entry in textEntries)
        {
            Destroy(entry);
        }
        textEntries.Clear();

        List<string> names = new List<string>();

        //get data based on current category
        switch (currentCategory)
        {
            case InventoryCategory.Polaroids:
                foreach (var polaroid in PolaroidSystem.Instance.GetPolaroidCollection())
                    names.Add(polaroid.descriptionText);
                break;

            case InventoryCategory.Items:
                foreach (var item in ItemSystem.Instance.GetItemCollection())
                    names.Add(item.itemType.ToString());
                break;

            case InventoryCategory.Tools:
                foreach (var tool in ToolSystem.Instance.GetToolCollection())
                    names.Add(tool.toolName);
                break;
        }

        //generate text elements for names
        for (int i = 0; i < names.Count; i++)
        {
            GameObject textObj = Instantiate(textTemplate, textListParent);
            TMP_Text tmpText = textObj.GetComponent<TMP_Text>();
            tmpText.text = names[i];
            tmpText.color = Color.white;
            textEntries.Add(textObj);
        }

        //reset selection
        selectedIndex = 0;
        //selectedIndex = Mathf.Clamp(lastSelectedIndex, 0, textEntries.Count - 1);
        HighlightSelection();
    }

    private void SwitchToNextCategory()
    {
        //currentCategory = (InventoryCategory)(((int)currentCategory + 1) % System.Enum.GetValues(typeof(InventoryCategory)).Length);
        //PopulateCategory(currentCategory);
        int totalCategories = System.Enum.GetValues(typeof(InventoryCategory)).Length;
        int startCategory = (int)currentCategory;

        //cycle to the next non-empty category
        for (int i = 1; i <= totalCategories; i++)
        {
            int nextCategory = (startCategory + i) % totalCategories;
            if (GetCategoryItemCount((InventoryCategory)nextCategory) > 0)
            {
                PopulateCategory((InventoryCategory)nextCategory);
                return;
            }
        }
    }
    private void MoveSelection(int direction)
    {
        if (textEntries.Count == 0) return;
        selectedIndex = (selectedIndex + direction) % textEntries.Count;

        HighlightSelection();
    }
    private void HighlightSelection()
    {
        for (int i = 0; i < textEntries.Count; i++)
        {
            TMP_Text tmpText = textEntries[i].GetComponent<TMP_Text>();
            tmpText.color = (i == selectedIndex) ? Color.white : Color.gray;
        }

        switch (currentCategory)
        {
            case InventoryCategory.Polaroids:
                PolaroidData polaroidDataObj = PolaroidSystem.Instance.GetPolaroidCollection()[selectedIndex];
                polaroidDescription.text = polaroidDataObj.descriptionText;
                polaroidInventoryImage.sprite = polaroidDataObj.imageSprite;
                break;
            case InventoryCategory.Items:
                ItemData itemDataObj = ItemSystem.Instance.GetItemCollection()[selectedIndex];
                objOrToolDescription.text = itemDataObj.descriptionText;
                objOrToolImage.sprite = itemDataObj.imageSprite;
                break;
            case InventoryCategory.Tools:
                ToolData toolDataObj = ToolSystem.Instance.GetToolCollection()[selectedIndex];
                objOrToolDescription.text = toolDataObj.descriptionText;
                objOrToolImage.sprite = toolDataObj.imageSprite;
                break;
        }
            
    }

    /// <summary>
    /// freeze camera movement by freezing cursor, also hide the cursor point in the middle of screen, freeze time. 
    /// </summary>
    private void FreezeCamera()
    {
        CursorUI.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.lockState = CursorLockMode.None;
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
