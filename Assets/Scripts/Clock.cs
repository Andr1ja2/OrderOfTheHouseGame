using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // Time intentionally stays the same on reset
    public int hour;         // 0 to 23
    public int minute;       // 0 to 59
    public string period;    // AM or PM

    public bool stop = false;

    private float timeAccumulator = 0f;
    private float secondsPerInGameMinute = 0.5f; // 30 sec per hour => 0.5 sec per minute



    private void Start()
    {
        hour = 18;
        minute = 0;
        UpdatePeriod();
    }

    private void Update()
    {
        if (stop) return;

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
        if (GameManager.instance.playerController.FacingDirection == Vector2.up)
        {
            AudioController.instance.PlaySFX(AudioController.instance.clockInteract);
            GameManager.instance.dialogueController.StartDialogue(new string[] { ("The clock reads " + GetFormattedTime()) });
        }
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
