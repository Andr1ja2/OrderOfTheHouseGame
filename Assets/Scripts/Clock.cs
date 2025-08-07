using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // Time intentionally stays the same on reset
    public int hour;         // 0 to 23
    public int minute;       // 0 to 59
    public string period;    // AM or PM

    private float timeAccumulator = 0f;
    private float secondsPerInGameMinute = 0.5f; // 30 sec per hour => 0.5 sec per minute

    private void Start()
    {
        hour = 6;
        minute = 0;
        UpdatePeriod();
    }

    private void Update()
    {
        timeAccumulator += Time.deltaTime;

        if (timeAccumulator >= secondsPerInGameMinute)
        {
            timeAccumulator -= secondsPerInGameMinute;
            AdvanceTimeByOneMinute();
        }
    }

    void AdvanceTimeByOneMinute()
    {
        minute++;

        if (minute >= 60)
        {
            minute = 0;
            hour++;

            if (hour >= 24)
                hour = 0;
        }

        if (GameManager.instance.roomController.currentRoom == GameManager.instance.roomController.livingRoom)
        {
            if (hour < 6) // Rule 1 00 to 06
            {
                string deathText = GameManager.instance.pinBoard.hasRule1 ? string.Empty : "Dad seems to get mad when I stay in the living room late at night";
                GameManager.instance.levelController.Death(deathText, 1);
            }
        }

        UpdatePeriod();
    }

    void UpdatePeriod()
    {
        if (hour < 12)
            period = "AM";
        else
            period = "PM";
    }

    public string GetFormattedTime()
    {
        int displayHour = hour % 12;
        if (displayHour == 0) displayHour = 12;
        return string.Format("{0:D2}:{1:D2} {2}", displayHour, minute, period);
    }

    public void DisplayTime()
    {
        GameManager.instance.dialogueController.StartDialogue(new string[] { ("The clock reads " + GetFormattedTime()) });
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction += DisplayTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction -= DisplayTime;
        }
    }
}
