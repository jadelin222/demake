using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public GameObject controlHintUI;
    public TMP_Text controlHintText;
    public GameObject bottomScreenUI;
    public TMP_Text bottomScreenText;
    public GameObject fullScreenUI;

    [Header("Time UI")]
    [SerializeField] public TMP_Text timeTxt;
    [SerializeField] private GameTime gameTime; 

    void Start()
    {

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
    public void ShowControlHintUI()
    {
        
    }
}
