using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // add getter and setter ?
    public InputController inputController;
    public DialogueController dialogueController;
    public InventoryController inventoryController;
    public RoomController roomController;
    public Clock clock;
    public PlayerController playerController;
    public LevelController levelController;
    public Lightswitch ligthswitch;
    public PinBoard pinBoard;
    //public Animator fadeEffectAnimator;
    public TextMeshProUGUI stepCounter;

    public Tilemap floorTilemap;
    public Tilemap[] collisionTilemaps;
    public Tilemap carpetTilemap;

    public UnityAction<KeyCode> MovePlayer;
    public UnityAction SkipDialogue;
    public UnityAction PushAction;
    public UnityAction DetectJunk;
    public UnityAction<GameObject> DetectRoom;
    public UnityAction ResetValues;
    public UnityAction PauseGame;

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

        InputController.actionsEnabled = true;
        PlayerController.stepCount = 0;
        JunkDetector.AllJunkDetectors = FindObjectsOfType<JunkDetector>();
    }
}
