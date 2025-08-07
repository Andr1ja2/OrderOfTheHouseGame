using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JunkScript : MonoBehaviour
{
    public JunkDetector JunkDetector;

    public void Move(Vector2 dir)
    {
        if (CanMove(dir))
        {
            transform.position += (Vector3)dir;

            // Refresh blocked sides for all
            JunkDetector.RefreshAllJunk();
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
