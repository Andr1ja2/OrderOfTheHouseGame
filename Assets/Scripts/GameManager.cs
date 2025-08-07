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
    public RoomController roomController;
    public Clock clock;
    public PlayerController playerController;
    public LevelController levelController;
    public Lightswitch ligthswitch;
    public PinBoard pinBoard;

    public Tilemap floorTilemap;
    public Tilemap[] collisionTilemaps;
    public Tilemap carpetTilemap;

    public UnityAction<KeyCode> MovePlayer;
    public UnityAction SkipDialogue;
    public UnityAction PushAction;
    public UnityAction DetectJunk;
    public UnityAction<GameObject> DetectRoom;
    public UnityAction ResetValues;

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
}
