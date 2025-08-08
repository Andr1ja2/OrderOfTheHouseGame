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
            InputController.actionsEnabled = true;
        }
    }

    public void PickUpNote(PaperNote note, int rulenumber)
    {
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
        noteCanvas.SetActive(true);
        InputController.actionsEnabled = false;
        Destroy(note.gameObject);
    }

    public void Death(string noteText, int rulenumber) // add fade effect
    {
        InputController.actionsEnabled = false;
        GameManager.instance.playerController.animator.enabled = false;
        GameManager.instance.fadeEffectAnimator.SetTrigger("fadeIn");
        GameObject spawnedNote = null;
        if (noteText != string.Empty)
        {
            spawnedNote = Instantiate(paperNotePrefab, this.transform);
            spawnedNote.transform.position = GameManager.instance.playerController.transform.position;
            spawnedNote.GetComponent<PaperNote>().noteText = noteText;
            spawnedNote.GetComponent<PaperNote>().rulenumber = rulenumber;
            spawnedNote.SetActive(false);
        }

        StartCoroutine(WaitForDeath(spawnedNote));
    }

    IEnumerator WaitForDeath(GameObject spawnedNote)
    {
        // Ensures screen is balck when player is moving back to start position
        yield return new WaitForSecondsRealtime(GameManager.instance.fadeEffectAnimator.GetCurrentAnimatorStateInfo(0).length); 
        GameManager.instance.ResetValues.Invoke();
        if (spawnedNote != null) spawnedNote.SetActive(true);
        JunkDetector.RefreshAllJunk();
        SceneManager.LoadScene("HouseScene");
        GameManager.instance.playerController.animator.enabled = true;
        GameManager.instance.fadeEffectAnimator.SetTrigger("fadeOut"); // FadeOut is called only after the scene is loaded
        InputController.actionsEnabled = true;
    }
}
