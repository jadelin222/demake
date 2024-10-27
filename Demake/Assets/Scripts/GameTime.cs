using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTime : MonoBehaviour
{
    [Header("Time")]
    public float dayLength = 600f; // in seconds
    public float inGameTime = 8f / 24f;
    //[Header("UI")]
    //[SerializeField]
    //private TMP_Text timeTxt; //move to ui manager

    void Update()
    {
        //timeTxt.text = GetTime();
        inGameTime += Time.deltaTime / dayLength;
        if (inGameTime >= 1)
        {
            inGameTime = 0;
        }

    }
    public string GetTime()
    {
        float hours = inGameTime * 24f;
        int hourInt = Mathf.FloorToInt(hours);
        int minutes = Mathf.FloorToInt((hours - hourInt) * 60);
        return $"{hourInt:D2}:{minutes:D2}";
    }
}
