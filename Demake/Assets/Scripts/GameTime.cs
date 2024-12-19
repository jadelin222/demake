using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTime : MonoBehaviour
{
    [Header("Time")]
    public float dayLength = 600f; // in seconds
    public float inGameTime = 8f / 24f;

    [Header("Days")]
    private string[] daysOfWeek = { "Friday", "Saturday", "Sunday" };
    private int currentDayIndex = 0;

    void Update()
    {
        //timeTxt.text = GetTime();
        inGameTime += Time.deltaTime / dayLength;
        if (inGameTime >= 1)
        {
            inGameTime = 0;
            currentDayIndex = (currentDayIndex + 1) % daysOfWeek.Length;
        }

    }
    public string GetTime()
    {
        float hours = inGameTime * 24f;
        int hourInt = Mathf.FloorToInt(hours);
        int minutes = Mathf.FloorToInt((hours - hourInt) * 60);
        return $"{hourInt:D2}:{minutes:D2}";
    }
    public string GetDay()
    {
        return daysOfWeek[currentDayIndex];
    }
    public void SetTimeToNextDay(float time)
    {
        inGameTime = time;
        currentDayIndex = (currentDayIndex + 1) % daysOfWeek.Length;
    }
}
