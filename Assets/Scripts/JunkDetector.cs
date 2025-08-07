using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkDetector : MonoBehaviour
{
    public static JunkDetector[] AllJunkDetectors;

    public List<Vector2> BlockedSides; // maybe not be needed because of the gameobjects for each junk ?
    public GameObject upJunk;
    public GameObject leftJunk;
    public GameObject downJunk;
    public GameObject rightJunk;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Junk"))
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            if (Vector3.Dot(direction, Vector2.up) > 0.9)
            {
                BlockedSides.Add(Vector2.up);
                upJunk = collision.gameObject;
            }
            else if (Vector2.Dot(direction, Vector2.down) > 0.9f)
            {
                BlockedSides.Add(Vector2.down);
                downJunk = collision.gameObject;
            }
            else if (Vector2.Dot(direction, Vector2.left) > 0.9f)
            {
                BlockedSides.Add(Vector2.left);
                leftJunk = collision.gameObject;
            }
            else if (Vector2.Dot(direction, Vector2.right) > 0.9f)
            {
                BlockedSides.Add(Vector2.right);
                rightJunk = collision.gameObject;
            }
        }
    }

    // Refresh blocked sides and detected junk
    public void RefreshJunk()
    {
        BlockedSides.Clear();
        upJunk = null;
        leftJunk = null;
        rightJunk = null;
        downJunk = null;
    }

    public static void RefreshAllJunk()
    {
        foreach (JunkDetector jd in AllJunkDetectors)
        {
            jd.RefreshJunk();
        }
    }
}
