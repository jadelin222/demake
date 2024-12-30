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
    public GameObject polaroidPrefab;
    public GameObject letterPrefab;
    public GameObject itemToolPrefab;
    public TMP_Text letterText;
    public TMP_Text pageNumberText;
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

    [Header("CutScene UI")]
    public GameObject cutSceneUI;
    public GameObject newDayUI;
    public TMP_Text newDayText;
    public GameObject endGameUI;
    public TMP_Text endGameText;
    public bool isCutsceneActive = false;

    private List<GameObject> textEntries = new List<GameObject>();

    private int selectedIndex = 0;
    private InventoryManager.InventoryCategory currentCategory;
    private bool isInventoryOpen = false;

    private Action onPickupClosed;  //callback to finalize pickup
    private Coroutine typewriterCoroutine;
    private float autoHideDelay = 2f; //typewriter

    private List<string> letterPages = new List<string>();
    private int currentPageIndex = 0;
    private int wordsPerPage = 100;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        //td: update current category according to last pickup 

        if (gameTime != null)
            timeTxt.text = gameTime.GetTime();
        if (Input.GetKeyDown(KeyCode.Tab) &&!isInventoryOpen)
                ShowInventory();
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
            HidePickupUI();
        //flip pages in the letter UI
        if (LetterUI.activeSelf && Input.GetKeyDown(KeyCode.RightArrow))
            FlipPage(1);
        if (LetterUI.activeSelf && Input.GetKeyDown(KeyCode.LeftArrow))
            FlipPage(-1);

    }
    /// <summary>
    /// bottom screenui and type writer fx
    /// </summary>
    public void ShowBottomScreenUI(string message)
    {
        //stop previous typewriter effect if running
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        bottomScreenUI.SetActive(true);
        typewriterCoroutine = StartCoroutine(TypewriterEffect(bottomScreenText, message, autoHideDelay));
    }
    private IEnumerator TypewriterEffect(TMP_Text textComponent, string message, float delayAfter = 0f)
    {
        textComponent.text = ""; //clear existing text
        string[] lines = message.Split('.');
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            textComponent.text = ""; //reset for each line
            
            foreach (char letter in line.Trim())
            {
                textComponent.text += letter;
                yield return new WaitForSeconds(0.05f); //typing effect speed
            }
            yield return new WaitForSeconds(1f); //pause after a line
        }

        //wait for the specified delay before disabling the UI
        if (delayAfter > 0f)
        {
            yield return new WaitForSeconds(delayAfter); 
            bottomScreenUI.SetActive(false);
        }
    }
    /// <summary>
    /// When attempt to pick up item, shows this ui to display info
    /// </summary>
    public void ShowPickupUI(PickupData uiData, Action onClose)
    //public void ShowPickupUI(PickupDisplayType displayType, string bottomMessage, Action onClose)
    {
        ToolSystem.Instance.PutAwayActiveTool();
        pickUpMaskUI.SetActive(true); //bring up the UI group for pickup interaction
        FreezeCamera();
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
        ResumeCamera();
        pickUpMaskUI.SetActive(false);

        //invoke the callback to finalize pickup
        Debug.Log("Invoking callback in HidePickupUI...");
        onPickupClosed?.Invoke();
        onPickupClosed = null;
        Debug.Log("Callback executed.");

        ResumeCamera();
    }
    //private IEnumerator AnimatePickUpPanel(GameObject UIPrefab,Vector3 startPosition, Vector3 endPosition, float duration)
    //private IEnumerator AnimatePickUpPanel(GameObject UIPrefab, float duration)
    //{
    //    float time = 0f;
    //    Vector3 startPosition = new Vector3(UIPrefab.transform.position.x, -Screen.height, UIPrefab.transform.position.z);
    //    Vector3 endPosition = UIPrefab.transform.position;
    //    Vector3 overshootPosition = endPosition + new Vector3(0, 50, 0); 

    //    while (time < duration)
    //    {
    //        float t = time / duration;
    //        float easedT = Ease(t);
    //        UIPrefab.transform.position = Vector3.Lerp(startPosition, overshootPosition, easedT);

    //        //UIPrefab.transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
    //        time += Time.unscaledDeltaTime;
    //        yield return null;
    //    }

    //    UIPrefab.transform.position = endPosition;
    //}
    private IEnumerator AnimatePickUpPanel(GameObject uiObject, float slideDuration)
    {
        float overshootAmount = 20f;//in px
        float wobbleDuration = 0.5f;
        float wobbleFrequency = 4f;
        float wobbleAmplitude = 15f;  

        //positions
        Vector3 endPosition = uiObject.transform.position;
        Vector3 startPosition = new Vector3(endPosition.x, -Screen.height, endPosition.z);
        Vector3 overshootPosition = endPosition + new Vector3(0, overshootAmount, 0);

        //slide from off-screen to overshoot pos
        float time = 0f;
        while (time < slideDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / slideDuration);

            uiObject.transform.position = Vector3.Lerp(startPosition, overshootPosition, t);

            yield return null;
        }

        //wobble from overshoot position down to end pos
        time = 0f;
        while (time < wobbleDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / wobbleDuration);

            //base interpolation between overshoot and end pos
            Vector3 basePos = Vector3.Lerp(overshootPosition, endPosition, t);

            //sine function wobble offset
            float sineValue = Mathf.Sin(t * wobbleFrequency * 2f * Mathf.PI);
            float damping = 1f - t;
            float wobbleOffset = sineValue * wobbleAmplitude * damping;

            uiObject.transform.position = basePos + new Vector3(0, wobbleOffset, 0);

            yield return null;
        }

        uiObject.transform.position = endPosition;
    }
    public void ShowItemOrToolUI(PickupData uiData)
    {
        ItemOrToolUI.SetActive(true);
        bottomLeftMessage.text = uiData.bottomMessage;
        closeHintText.text = $"Q - {uiData.actionVerb}";
        StartCoroutine(AnimatePickUpPanel(itemToolPrefab, 0.3f));

    }
    public void ShowLetterUI(PickupData uiData)
    {
        LetterUI.SetActive(true);
        bottomLeftMessage.text = uiData.bottomMessage;
        //letterText.text = uiData.descriptionText;
        closeHintText.text = $"Q - {uiData.actionVerb}";

        letterPages = ChopTextIntoPages(uiData.descriptionText, wordsPerPage);
        currentPageIndex = 0;
        DisplayCurrentPage();
        StartCoroutine(AnimatePickUpPanel(letterPrefab, 0.3f));

    }
    private List<string> ChopTextIntoPages(string text, int wordsPerPage)
    {
        List<string> pages = new List<string>();
        string[] words = text.Split(' ');
        int wordCount = 0;
        string currentPage = "";

        foreach (string word in words)
        {
            if (wordCount + word.Length > wordsPerPage)
            {
                pages.Add(currentPage.Trim());
                currentPage = "";
                wordCount = 0;
            }
            currentPage += word + " ";
            wordCount += word.Length;
        }

        if (!string.IsNullOrEmpty(currentPage.Trim()))
        {
            pages.Add(currentPage.Trim());
        }

        return pages;
    }
    private void DisplayCurrentPage()
    {
        if (currentPageIndex >= 0 && currentPageIndex < letterPages.Count)
        {
            letterText.text = letterPages[currentPageIndex];
            pageNumberText.text = $"page {currentPageIndex + 1} / {letterPages.Count} "; 
        }
    }
    private void FlipPage(int direction)
    {
        currentPageIndex += direction;
        currentPageIndex = Mathf.Clamp(currentPageIndex, 0, letterPages.Count - 1);
        DisplayCurrentPage();
    }


    public void ShowPolaroidUI(PickupData uiData)
    {
        PolaroidUI.SetActive(true);
        bottomLeftMessage.text = uiData.bottomMessage;
        closeHintText.text = $"Q - {uiData.actionVerb}";

        polaroidImage.sprite = uiData.imageSprite;
        polaroidText.text = uiData.descriptionText;

        //Vector3 startPosition = new Vector3(polaroidPrefab.transform.position.x, -Screen.height, polaroidPrefab.transform.position.z);
        //Vector3 endPosition = polaroidPrefab.transform.position;
        StartCoroutine(AnimatePickUpPanel(polaroidPrefab, 0.3f));
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
        if (pickUpMaskUI.activeSelf) return; //prevent opening inventory if PickUp UI is active
        if (InventoryManager.Instance.IsInventoryEmpty()) return; //prevent opening inventory if empty

        FreezeCamera(); 
        inventoryUI.SetActive(true);
        isInventoryOpen = true;
        PopulateCategory(InventoryManager.Instance.FindFirstNonEmptyCategory());
        //PopulateCategory(lastCategory);
    }
    public void CloseInventory()
    {
        ResumeCamera();
        inventoryUI.SetActive(false);

        isInventoryOpen = false;
    }
    private void PopulateCategory(InventoryManager.InventoryCategory category)
    {
        currentCategory = category;
        //int itemCount = GetCategoryItemCount(category);
        int itemCount = InventoryManager.Instance.GetCategoryCount(category);
        if (itemCount == 0) return;

        //update the category title
        categoryTitle.text = category.ToString() + "  >";

        //toggle the correct display
        switch (category)
        {
            case InventoryManager.InventoryCategory.Polaroids:
                displayPolaroid.SetActive(true);
                displayObjOrTool.SetActive(false);
                StartCoroutine(AnimatePickUpPanel(displayPolaroid, 0.5f));
                break;

            case InventoryManager.InventoryCategory.Items:
            case InventoryManager.InventoryCategory.Tools:
                displayPolaroid.SetActive(false);
                displayObjOrTool.SetActive(true);
                StartCoroutine(AnimatePickUpPanel(displayObjOrTool, 0.5f));
                break;
        }

        PopulateTextList();
    }
    private void PopulateTextList()
    {
        //clear old text entries
        foreach (var entry in textEntries)
        {
            Destroy(entry);
        }
        textEntries.Clear();

        //List<string> names = new List<string>();
        List<string> names = InventoryManager.Instance.GetNames(currentCategory);

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
        int totalCategories = System.Enum.GetValues(typeof(InventoryManager.InventoryCategory)).Length;
        int startCategory = (int)currentCategory;

        //cycle to the next non-empty category
        for (int i = 1; i <= totalCategories; i++)
        {
            int nextCategory = (startCategory + i) % totalCategories;
            if (InventoryManager.Instance.GetCategoryCount((InventoryManager.InventoryCategory)nextCategory) > 0)
            {
                PopulateCategory((InventoryManager.InventoryCategory)nextCategory);
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
        var metaData = InventoryManager.Instance.GetMetaData(currentCategory, selectedIndex);
        switch (currentCategory)
        {
            case InventoryManager.InventoryCategory.Polaroids:
                PolaroidData polaroidDataObj = (PolaroidData)metaData;
                polaroidDescription.text = polaroidDataObj.descriptionText;
                polaroidInventoryImage.sprite = polaroidDataObj.imageSprite;
                break;
            case InventoryManager.InventoryCategory.Items:
                ItemData itemDataObj = (ItemData)metaData;
                objOrToolDescription.text = itemDataObj.descriptionText;
                objOrToolImage.sprite = itemDataObj.imageSprite;
                break;
            case InventoryManager.InventoryCategory.Tools:
                ToolData toolDataObj = (ToolData)metaData;
                objOrToolDescription.text = toolDataObj.descriptionText;
                objOrToolImage.sprite = toolDataObj.imageSprite;
                break;
        }     
    }
    /// <summary>
    /// game over UI for win/lose
    /// </summary>
    public void ShowCutSceneUI()
    {
        cutSceneUI.SetActive(true);
    }
    public void ShowDayCutScene(string day)
    {
        if (isCutsceneActive) return; //prevent starting another cutscene if already active
        //FadeManager.Instance.FadeIn();
        FadeManager.Instance.FadeIn(() =>
        {
            isCutsceneActive = true;
            FreezeCamera();
            ShowCutSceneUI();
            newDayUI.SetActive(true);
            endGameUI.SetActive(false);
            newDayText.text = day;
            StartCoroutine(HideDayCutSceneAfterDelay(2f));
        });
    }
    private IEnumerator HideDayCutSceneAfterDelay(float delay)
    {
        //yield return new WaitForSeconds(delay);
        yield return new WaitForSecondsRealtime(delay);
        FadeManager.Instance.FadeOut();
        newDayUI.SetActive(false);
        cutSceneUI.SetActive(false);
        ResumeCamera();
        isCutsceneActive = false;
    }
    public void ShowGameOverUI(string message)
    {
        ShowCutSceneUI();
        newDayUI.SetActive(false);
        endGameUI.SetActive(true);
        StartCoroutine(PlayGameOverText(message));
    }

    private IEnumerator PlayGameOverText(string message)
    {
        yield return TypewriterEffect(endGameText, message, 1.5f); //use the typewriter effect with a delay after
        //restartHint.SetActive(true);
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
