using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    bool actionsEnabled;
    bool paused = false;

    private void Start()
    {
        GameManager.instance.PauseGame += Pause;
    }

    public void QuitToMainMenuButton()
    {
        StartCoroutine(QuitToMainMenu());
    }

    public IEnumerator QuitToMainMenu()
    {
        Time.timeScale = 1f;
        FadeEffectAnimator.instance.FadeIn();
        yield return new WaitForSecondsRealtime(FadeEffectAnimator.instance.animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(GameManager.instance.gameObject); // reset level
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Pause()
    {
        if (!paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            actionsEnabled = InputController.actionsEnabled;
            InputController.actionsEnabled = false;
            paused = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            InputController.actionsEnabled = actionsEnabled;
            paused = false;
        }
    }
}
