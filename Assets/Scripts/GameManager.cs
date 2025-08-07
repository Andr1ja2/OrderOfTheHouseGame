using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // add getter and setter
    public InputController inputController;
    public DialogueController dialogueController;
    public InventoryController inventoryController;
    //public PlayerController playerController;

    public Tilemap floorTilemap;
    public Tilemap[] collisionTilemaps;

    public UnityAction<KeyCode> MovePlayer;
    public UnityAction SkipDialogue;
    public UnityAction PushAction;
    public UnityAction DetectJunk;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        JunkDetector.AllJunkDetectors = FindObjectsOfType<JunkDetector>();
    }

    private void Update()
    {
        
    }
}
