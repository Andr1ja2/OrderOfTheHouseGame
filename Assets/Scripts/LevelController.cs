using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script mangaes death, paper notes and such
public class LevelController : MonoBehaviour
{
    public GameObject paperNotePrefab;

    public GameObject noteCanvas;
    public TextMeshProUGUI noteTextUI;
    public GameObject headerText;
    public Transform noteSpawnpoint;

    bool actionsOn = true;
    bool firstPickup = true;

    private void Start()
    {
        if (noteTextUI == null) noteTextUI = noteCanvas.GetComponentInChildren<TextMeshProUGUI>();
        GameManager.instance.SkipDialogue += NoteOff;
    }

    private void NoteOff()
    {
        if (noteCanvas.activeSelf)
        {
            noteCanvas.SetActive(false);
            InputController.actionsEnabled = actionsOn;

            PaperNote existingNote = GetComponentInChildren<PaperNote>(true);
            if (existingNote != null)
            {
                existingNote.gameObject.SetActive(true);
                return;
            }

            if (firstPickup)
            {
                GameManager.instance.dialogueController.StartDialogue(new string[] { "This probably belongs on dad's pinboard.", "I should check it out."});
                firstPickup = false;
            }
        }
    }

    public void PickUpNote(PaperNote note, int rulenumber)
    {
        AudioController.instance.PlaySFX(AudioController.instance.pickupNote);

        switch (rulenumber)
        {
            case 1:
                GameManager.instance.pinBoard.hasRule1 = true;
                break;
            case 2:
                GameManager.instance.pinBoard.hasRule2 = true;
                break;
            case 3:
                GameManager.instance.pinBoard.hasRule3 = true;
                break;
            default:
                Debug.LogError("Invalid number passed to LevelController.PickUpNote()");
                return;
        }
        noteTextUI.text = note.noteText;
        headerText.SetActive(false);
        noteCanvas.SetActive(true);
        GameManager.instance.pinBoard.glow.SetActive(true);
        actionsOn = InputController.actionsEnabled;
        InputController.actionsEnabled = false;
        Destroy(note.gameObject);
    }

    public void DeathButton() { Death(); }

    public void Death(string noteText = "", int rulenumber = 0)
    {
        InputController.actionsEnabled = false;
        GameManager.instance.playerController.animator.enabled = false;
        FadeEffectAnimator.instance.FadeIn();
        GameObject spawnedNote = null;
        if (noteText != string.Empty)
        {
            PaperNote[] existingNotes = this.GetComponentsInChildren<PaperNote>();
            foreach(PaperNote note in existingNotes) note.gameObject.SetActive(false);
            spawnedNote = Instantiate(paperNotePrefab, noteSpawnpoint.position, Quaternion.identity);
            spawnedNote.transform.SetParent(this.transform, true);
            spawnedNote.GetComponent<PaperNote>().noteText = noteText;
            spawnedNote.GetComponent<PaperNote>().rulenumber = rulenumber;
            spawnedNote.SetActive(false);
        }

        AudioController.instance.PlaySFX(AudioController.instance.death);
        StartCoroutine(WaitForDeath(spawnedNote));
    }

    IEnumerator WaitForDeath(GameObject spawnedNote)
    {
        // Ensures screen is balck when player is moving back to start position
        yield return new WaitForSecondsRealtime(FadeEffectAnimator.instance.animator.GetCurrentAnimatorStateInfo(0).length);
        GameManager.instance.ResetValues.Invoke();
        if (spawnedNote != null) spawnedNote.SetActive(true);
        JunkDetector.RefreshAllJunk();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.playerController.animator.enabled = true;
        //FadeEffectAnimator.instance.FadeOut(); // FadeOut is called only after the scene is loaded
        InputController.actionsEnabled = true;
    }
}
