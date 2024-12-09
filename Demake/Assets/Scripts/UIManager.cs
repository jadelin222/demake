using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [Header("Control Hint UI")]
    public GameObject controlHintUI;
    public TMP_Text controlHintText;

    [Header("Bottom Screen UI")]
    public GameObject bottomScreenUI;
    public TMP_Text bottomScreenText;

    [Header("Full Screen UI")]
    public GameObject fullScreenUI;

    [Header("Time UI")]
    [SerializeField] public TMP_Text timeTxt;
    [SerializeField] private GameTime gameTime;

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

    }
    public void ShowBottomScreenUI()
    {
        //with type writer fx
    }
    public void ShowFullScreenUI()
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
}
