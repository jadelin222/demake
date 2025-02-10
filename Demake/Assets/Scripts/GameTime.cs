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
    private string[] daysOfWeek = { "Friday", "Saturday", "Sunday", "Monday" };
    private int currentDayIndex = 0;

    void Update()
    {
        inGameTime += Time.deltaTime / dayLength;
        if (inGameTime >= 1)
        {
            inGameTime = 0;
            currentDayIndex = (currentDayIndex + 1) % daysOfWeek.Length;
        }
        CheckForLateNight();

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
    //game over if 2 am.
    private void CheckForLateNight()
    {
        float hours = inGameTime * 24f;
        int hourInt = Mathf.FloorToInt(hours);

        if (hourInt == 2)
        {
            GameMaster.Instance.TriggerEnding(GameMaster.Endings.StayedUpLate);
        }
    }
}
