using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public enum Endings
    {
        CaughtByGhosts,
        CutTooManyFlowers,
        Win
    }
    public static GameMaster Instance;

    private bool isGameOver = false;

    private void Awake()
    {
            Instance = this;
    }
    private void Update()
    {
        if (!isGameOver) return;
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //restart game
                RestartGame();
            }
        }
    }

    public void TriggerEnding(Endings ending)
    {
        if (isGameOver) return; //prevent multiple triggers
        isGameOver = true;

        string message = ending switch
        {
            Endings.CaughtByGhosts => "Game Over, You were caught by ghosts. Hahahaha. Press Q to Restart",
            Endings.CutTooManyFlowers => "Game Over, You cut too many flowers. Hahahaha! Press Q to Restart",
            Endings.Win => "You Win! Congratulations! Press Q to Restart",
            _ => "Game Over"
        };

        //UIManager.Instance.ShowGameOverUI(message);
        FadeManager.Instance.FadeIn(() => UIManager.Instance.ShowGameOverUI(message));
    }

    private void RestartGame()
    {
        //reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
