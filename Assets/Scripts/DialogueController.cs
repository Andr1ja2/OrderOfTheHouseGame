using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    // List all dialoguebox objects here
    public GameObject DialogueBox;
    public Dialogue dialogue;

    private void Start()
    {
        dialogue = DialogueBox.GetComponent<Dialogue>();
    }

    public bool DialogueIsActive()
    {
        return DialogueBox.GetComponent<Image>().color.a != 0;
    }

    //private void Start() this is no good, use colliders or sth
    //{
    //    GameManager.instance.PushAction += PushDialogue;
    //    startingDialogue.SetActive(true);
    //}

    /// Dialogue starter is also no good, try to make singular dialogue box and pass it lines somehow; idk

    //private void PushDialogue()
    //{
    //    // pushDialogue.SetActive(true); !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //    GameManager.instance.PushAction -= PushDialogue;
    //}

   
}
