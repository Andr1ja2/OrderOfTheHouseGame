using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] public static bool actionsEnabled = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.PauseGame.Invoke();
        }
        else if (actionsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                GameManager.instance.MovePlayer.Invoke(KeyCode.W);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                GameManager.instance.MovePlayer.Invoke(KeyCode.A);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                GameManager.instance.MovePlayer.Invoke(KeyCode.S);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                GameManager.instance.MovePlayer.Invoke(KeyCode.D);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                GameManager.instance.PushAction.Invoke();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.instance.SkipDialogue.Invoke();
        }
    }
}
