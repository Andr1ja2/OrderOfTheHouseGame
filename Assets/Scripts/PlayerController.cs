using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Vector2 FacingDirection;
    public JunkDetector JunkDetector;

    Animator animator;
    SpriteRenderer spriteRenderer;

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
        FacingDirection = Vector2.down;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameManager.instance.MovePlayer += ReturnDirection;
        GameManager.instance.PushAction += PushJunk;
    }

    private void ReturnDirection(KeyCode key)
    {
        Vector2 dir = Vector2.right;
        switch(key)
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
        if (CanMove(dir))
        {
            animator.SetTrigger("Move");
            transform.position += (Vector3)dir;

            JunkDetector.RefreshJunk();
        }
    }

    private bool CanMove(Vector2 dir)
    {
        // Check if the side it's moving to is blocked
        if (JunkDetector.BlockedSides.Contains(dir)) return false;

        // Checks if the tile it's moving to has a floor tile and no collision tile
        Vector3Int gridPosition = GameManager.instance.floorTilemap.WorldToCell(transform.position + (Vector3)dir);
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
}
