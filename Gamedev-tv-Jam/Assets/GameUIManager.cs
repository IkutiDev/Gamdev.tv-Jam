using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winGamePanel;
    public void TryAgain()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GammeOverScreen()
    {
        gameOverPanel.SetActive(true);
    }
    public void WinGame()
    {
        winGamePanel.SetActive(true);
    }
}
