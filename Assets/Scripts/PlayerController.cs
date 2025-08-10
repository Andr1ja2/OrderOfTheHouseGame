using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    public Vector2 FacingDirection;
    public JunkDetector JunkDetector;
    public Animator animator;
    public int maximumSteps = 30;

    SpriteRenderer spriteRenderer;

    public static int stepCount = 0;

    Vector3 startposition;
    bool die;

    private void PushJunk()
    {
        if (FacingDirection == Vector2.up)
        {
            if (JunkDetector.upJunk != null)
            {
                JunkDetector.upJunk.GetComponent<JunkScript>().Move(Vector2.up);
            }
        }
        else if (FacingDirection == Vector2.left)
        {
            if (JunkDetector.leftJunk != null)
            {
                JunkDetector.leftJunk.GetComponent<JunkScript>().Move(Vector2.left);
            }
        }
        else if (FacingDirection == Vector2.down)
        {
            if (JunkDetector.downJunk != null)
            {
                JunkDetector.downJunk.GetComponent<JunkScript>().Move(Vector2.down);
            }
        }
        else if (FacingDirection == Vector2.right)
        {
            if (JunkDetector.rightJunk != null)
            {
                JunkDetector.rightJunk.GetComponent<JunkScript>().Move(Vector2.right);
            }
        }
        // the object refreshes all junk if it moves (via static function)
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("X", FacingDirection.x);
        animator.SetFloat("Y", FacingDirection.y);
        spriteRenderer.flipX = (FacingDirection.x < 0);
    }

    private void Start()
    {
        startposition = transform.position;
        FacingDirection = Vector2.down;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameManager.instance.MovePlayer += ReturnDirection;
        GameManager.instance.PushAction += PushJunk;
        GameManager.instance.ResetValues += ResetValues;
    }

    private void ReturnDirection(KeyCode key)
    {
        Vector2 dir = Vector2.right;
        switch (key)
        {
            case KeyCode.W:
                dir = Vector2.up;
                break;
            case KeyCode.A:
                dir = Vector2.left;
                break;
            case KeyCode.S:
                dir = Vector2.down;
                break;
            default:
                dir = Vector2.right;
                break;

        }
        FacingDirection = dir;
        UpdateAnimator();
        Move(dir);
    }

    private void Move(Vector2 dir)
    {
        FacingDirection = dir;
        if (CanMove(dir))
        {
            StartCoroutine(MoveRoutine(dir));
        }
    }

    private bool CanMove(Vector2 dir)
    {
        // Check if the side it's moving to is blocked
        if (JunkDetector.BlockedSides.Contains(dir)) return false;

        Vector3Int gridPosition = GameManager.instance.floorTilemap.WorldToCell(transform.position + (Vector3)dir);
        // Set bool to kill player if he steps on a carpet with muddy boots
        if (GameManager.instance.carpetTilemap.HasTile(gridPosition) && GameManager.instance.inventoryController.hasBoots) die = true;

        // Checks if the tile it's moving to has a floor tile and no collision tile
        if (!GameManager.instance.floorTilemap.HasTile(gridPosition))
        {
            return false;
        }
        foreach (Tilemap tm in GameManager.instance.collisionTilemaps)
        {
            if (tm.HasTile(gridPosition)) return false;
        }

        return true;
    }

    public void ResetValues()
    {
        FacingDirection = Vector2.down;
        transform.position = startposition;
        stepCount = 0;
        GameManager.instance.stepCounter.text = stepCount.ToString("D2");

        die = false;
    }

    private IEnumerator MoveRoutine(Vector2 dir)
    {
        AudioController.instance.PlaySFX(AudioController.instance.pushJunk);
        animator.SetTrigger("Move");
        transform.position += (Vector3)dir;
        stepCount++;

        // Wait for roomdetection so player can't die when crossing rooms
        yield return new WaitForFixedUpdate();

        GameManager.instance.stepCounter.text = stepCount.ToString("D2");
        JunkDetector.RefreshJunk();

        if (PlayerPrefs.GetInt("cheatsOn", 0) == 0)
        {
            if (die)
            {
                string deathText = GameManager.instance.pinBoard.hasRule3 ? string.Empty : "Dad doesn't like it when I get mud on the carpets";
                GameManager.instance.levelController.Death(deathText, 3);
            }
            else if (stepCount > maximumSteps)
            {
                string deathText = GameManager.instance.pinBoard.hasRule2 ? string.Empty : "I think dad gets really mad when I make a lot of noise";
                GameManager.instance.levelController.Death(deathText, 2);
            }
            else if (GameManager.instance.clock.hour < 6)
            {
                if (GameManager.instance.roomController.currentRoom == GameManager.instance.roomController.livingRoom ||
                    GameManager.instance.roomController.currentRoom == GameManager.instance.roomController.kitchenRoom)
                {
                    string deathText = GameManager.instance.pinBoard.hasRule1 ? string.Empty : "Dad seems to get mad when I stay in the living room late at night";
                    GameManager.instance.levelController.Death(deathText, 1);
                }
            }
        }
    }
}
